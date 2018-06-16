using IdentityAPI.Repository;
using SimpleCommerce.Core.Dto;
using System.Collections.Generic;

namespace IdentityAPI.Services
{
    public class FakeUserService : IUserService
    {
        private IUserRepository _userRepository;
        public FakeUserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public List<UserDto> GetAllUsers()
        {
            var res = new List<UserDto>();
            foreach (var item in _userRepository.GetAll())
            {
                var user = new UserDto()
                {
                    UserId = item.UserId,
                    UserName = item.UserName,
                    Email = item.Email,
                    Gender = item.Gender
                };
                res.Add(user);
            }
            return res;
        }

        public UserDto SignIn(UserDto user)
        {
            if (user == null) return null;
            if(string.IsNullOrEmpty(user.UserId)|| string.IsNullOrEmpty(user.Password))
            {
                return null;
            }
            var entity = _userRepository.Get(user.UserId, user.Password);
            if (entity == null) return null;
            var res = new UserDto()
            {
                UserName = entity.UserName,
                UserId = entity.UserId,
                Email = entity.Email,
                Gender = entity.Gender
            };
            return res;
        }
    }
}
