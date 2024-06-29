using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserStorageService.Models;

namespace UserStorageService.Interfaces
{
    public interface IUserFacade
    {
        IEnumerable<User> GetAllUsers();

        bool UserExists(Guid id);

        User GetUserByNickname(string Nickname);

        User GetUserByEmail(string Email);

        User GetUserById(Guid Id);

        bool TryDeleteUserById(Guid Id);
    }
}