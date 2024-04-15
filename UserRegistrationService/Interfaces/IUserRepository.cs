using System.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserRegistrationService.Models;

namespace UserRegistrationService.Interfaces
{
    public interface IUserRepository
    {
        IEnumerable<User> GetAllUsers();

        User GetUserById(Guid id); //GRPC Client

        // bool TryGetUserByMail(string mail, out UserMailDto userMail); //Dto

        bool TryCreateUser(User user); //GRPC Client

        public bool SaveChange();
    }
}