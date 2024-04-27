using Application.Abstractions.Repositories.Users;
using Application.Utilities.Exceptions.Commons;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Services.Users
{
    public class UserBusinessRules
    {
        private readonly IUserReadRepository _userReadRepository;

        public UserBusinessRules(IUserReadRepository userReadRepository)
        {
            _userReadRepository = userReadRepository;
        }

        public async Task CheckEmailDuplicate(string email)
        {
            bool result = await _userReadRepository.Table
                .AnyAsync(x=> x.Email == email);

            if (result)
                throw new DuplicateException("Email", email);
        }
    }
}
