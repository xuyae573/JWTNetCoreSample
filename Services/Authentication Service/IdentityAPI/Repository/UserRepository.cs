using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityAPI.Domain;

namespace IdentityAPI.Repository
{
    public class FakeUserRepository : IUserRepository
    {

        private List<User> Users = new List<User>();
        public FakeUserRepository()
        {
            Users.Add(new User {
                UserId = "john1",
                UserName = "John",
                Password = "test123",
                Email = "john@example.com",
                Gender = "Male",
            });
            Users.Add(new User
            {
                UserId = "yaxu1",
                UserName = "Arthur",
                Password = "test123",
                Email = "yaxu1@example.com",
                Gender = "Male",
            });
            Users.Add(new User
            {
                UserId = "emma888",
                UserName = "Emma",
                Password = "test123",
                Email = "emma888@example.com",
                Gender = "Female",
            });
        }

        public User Get(string userId, string password)
        {
            return Users.FirstOrDefault(x => x.UserId == userId && x.Password == password);
        }

        public List<User> GetAll()
        {
            return Users;
        }
    }
}
