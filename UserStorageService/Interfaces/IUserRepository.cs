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

        public bool CheckingUserByNickname(string Nickname);

        public bool CheckingUserByEmail(User User);

        public bool TryFindUserByNickName(string name, out User User);

        public bool TryFindUserByEmail(string Email, out User User);

        public bool TryFindUserById(Guid Id, out User User);

        public bool TryDeleteUserById(Guid Id);

        IEnumerable<User> GetAllUsers();
    }
}