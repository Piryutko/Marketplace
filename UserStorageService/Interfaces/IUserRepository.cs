using System.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserStorageService.Models;

namespace UserStorageService.Interfaces
{
    public interface IUserRepository
    {
        bool AddUser(User user);

        public bool SaveChange();

        public bool UserExists(Guid Id);

        public bool CheckingUserByNickname(User User);

        public bool CheckingUserByEmail(User User);

        IEnumerable<User> GetAllUsers();
    }
}