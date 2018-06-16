using IdentityAPI.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityAPI.Repository
{
    public interface IUserRepository
    {
        List<User> GetAll();

        User Get(string userId, string password);
    }
}
