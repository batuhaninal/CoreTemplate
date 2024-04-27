using Application.Abstractions.Commons.Results;
using Application.Abstractions.Commons.Security;
using Application.Abstractions.Commons.Tokens;
using Application.Abstractions.Repositories.Commons;
using Application.Abstractions.Services.Auths;
using Application.Models.Constants.Roles;
using Application.Models.DTOs.Auths;
using Application.Models.DTOs.Commons.Results;
using Application.Models.Messages;
using Application.Models.Tokens;
using Application.Utilities.Exceptions.Commons;
using AutoMapper;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Services.Commons;
using Persistence.Services.Users;

namespace Persistence.Services.Auths
{
    public class AuthService : BaseService, IAuthService
    {
        private readonly ITokenService _tokenService;
        private readonly IHashingService _hashingService;
        private readonly UserBusinessRules _userBusinessRules;

        public AuthService(IUnitOfWork unitOfWork, IMapper mapper, IHashingService hashingService, ITokenService tokenService) : base(unitOfWork, mapper)
        {
            _hashingService = hashingService;
            _tokenService = tokenService;
            _userBusinessRules = new UserBusinessRules(unitOfWork.UserReadRepository);
        }

        public async Task<JwtToken> LoginAsync(LoginDto loginDto)
        {
            var user = await UnitOfWork.UserReadRepository.Table
                .Include(x=> x.UserRoles)!
                    .ThenInclude(x=> x.Role)
                .FirstOrDefaultAsync(x => x.Email == loginDto.Email);

            if (user == null)
                throw new BusinessException(CommonMessage.BusinessMessages.WrongPassword);

            if(!_hashingService.VerifyPassword(loginDto.Password, user.PasswordHash, user.PasswordSalt))
                throw new BusinessException(CommonMessage.BusinessMessages.WrongPassword);

            return _tokenService.CreateAccessToken(user, 15);
        }

        public async Task<IBaseResult> RegisterUserAsync(RegisterDto registerDto)
        {
            await _userBusinessRules.CheckEmailDuplicate(registerDto.Email);

            byte[] passwordHash, passwordSalt;
            _hashingService.CreatePasswordHash(registerDto.Password, out passwordHash, out passwordSalt);

            User newUser = Mapper.Map<User>(registerDto);
            newUser.PasswordHash = passwordHash;
            newUser.PasswordSalt = passwordSalt;

            var userId = await UnitOfWork.UserWriteRepository.CreateAsync(newUser);

            await UnitOfWork.UserRoleWriteRepository.CreateAsync(new UserRole()
            {
                UserId = userId,
                RoleId = Guid.Parse(AppRoles.User)
            });

            await UnitOfWork.SaveChangesAsync();

            return new SuccessResultDto(201, "User has been created. Please sign in.");
        }
    }
}
