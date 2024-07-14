using Application.Abstractions.Commons.Caching;
using Application.Abstractions.Commons.MessageBrokers.Publishers;
using Application.Abstractions.Commons.Results;
using Application.Abstractions.Commons.Security;
using Application.Abstractions.Commons.Tokens;
using Application.Abstractions.Repositories.Commons;
using Application.Abstractions.Services.Auths;
using Application.Models.Constants.Roles;
using Application.Models.DTOs.Auths;
using Application.Models.DTOs.Commons.Results;
using Application.Models.DTOs.Writers;
using Application.Models.Messages;
using Application.Models.Tokens;
using Application.Utilities.Exceptions.Commons;
using AutoMapper;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Services.Commons;
using Persistence.Services.Users;
using Persistence.Services.Writers;

namespace Persistence.Services.Auths
{
    public class AuthService : BaseService, IAuthService
    {
        private readonly ITokenService _tokenService;
        private readonly IHashingService _hashingService;
        private readonly UserBusinessRules _userBusinessRules;
        private readonly WriterBusinessRules _writerBusinessRules;

        public AuthService(IUnitOfWork unitOfWork, IMapper mapper, ICacheService cache, IRabbitMQPublisherService rabbitMQPublisherService, ITokenService tokenService, IHashingService hashingService) : base(unitOfWork, mapper, cache, rabbitMQPublisherService)
        {
            _hashingService = hashingService;
            _tokenService = tokenService;
            _userBusinessRules = new UserBusinessRules(unitOfWork.UserReadRepository);
            _writerBusinessRules = new WriterBusinessRules(unitOfWork.WriterReadRepository);
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
            using (var transaction = UnitOfWork.BeginTransaction())
            {
                try
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

                    await transaction.CommitAsync();

                    return new SuccessResultDto(201, "User has been created. Please sign in.");
                }
                catch (Exception)
                {
                    await transaction.RollbackAsync();
                    throw;
                }
            }

            
        }

        public async Task<IBaseResult> RegisterWriterAsync(RegisterWriterDto registerWriterDto)
        {
            using (var transaction = UnitOfWork.BeginTransaction())
            {
                try
                {
                    await _userBusinessRules.CheckEmailDuplicate(registerWriterDto.Email);
                    await _writerBusinessRules.CheckNickAvailable(registerWriterDto.Nick);

                    byte[] passwordHash, passwordSalt;
                    _hashingService.CreatePasswordHash(registerWriterDto.Password, out passwordHash, out passwordSalt);

                    User newUser = Mapper.Map<User>(registerWriterDto);
                    newUser.PasswordHash = passwordHash;
                    newUser.PasswordSalt = passwordSalt;

                    var userId = await UnitOfWork.UserWriteRepository.CreateAsync(newUser);

                    await UnitOfWork.UserRoleWriteRepository.CreateAsync(new UserRole()
                    {
                        UserId = userId,
                        RoleId = Guid.Parse(AppRoles.Writer)
                    });

                    Writer newWriter = Mapper.Map<Writer>(registerWriterDto);
                    newWriter.UserId = userId;

                    await UnitOfWork.WriterWriteRepository.CreateAsync(newWriter);

                    await UnitOfWork.SaveChangesAsync();

                    await transaction.CommitAsync();

                    return new SuccessResultDto(201);
                }
                catch (Exception)
                {
                    await transaction.RollbackAsync();
                    throw;
                }
            }
        }
    }
}
