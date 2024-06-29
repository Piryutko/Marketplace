using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserStorageService.Interfaces;
using UserStorageService.Models;

namespace UserStorageService.Facades
{
    public class UserFacade : IUserFacade
    {
        private readonly IUserRepository _userRepository;

        public UserFacade(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public bool TryDeleteUserById(Guid Id)
        {
            return _userRepository.TryDeleteUserById(Id);
        }

        public IEnumerable<User> GetAllUsers()
        {
            return _userRepository.GetAllUsers();
        }

        public User GetUserByEmail(string Email)
        {
            var result = _userRepository.TryFindUserByEmail(Email, out User user);

           if(result != null)
           {
                return user;
           }

           return user;
        }

        public User GetUserById(Guid Id)
        {
            var result = _userRepository.TryFindUserById(Id, out User user);

           if(result != null)
           {
                return user;
           }

           return user;
        }

        public User GetUserByNickname(string Nickname)
        {
            var result = _userRepository.TryFindUserByNickName(Nickname, out User user);

           if(result != null)
           {
                return user;
           }

           return user;
        }

        public bool UserExists(Guid id)
        {
            return _userRepository.UserExists(id);
        }
    }
}