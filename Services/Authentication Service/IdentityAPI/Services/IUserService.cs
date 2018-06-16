using SimpleCommerce.Core.Dto;
using System.Collections.Generic;

namespace IdentityAPI.Services
{
    public interface IUserService
    {
        UserDto SignIn(UserDto user);

        List<UserDto> GetAllUsers();
    }
}
