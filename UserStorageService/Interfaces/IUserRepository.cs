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

        IEnumerable<User> GetAllUsers();
    }
}