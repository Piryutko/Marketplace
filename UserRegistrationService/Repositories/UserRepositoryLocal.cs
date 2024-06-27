using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserRegistrationService.Interfaces;
using UserRegistrationService.Models;

namespace UserRegistrationService.Repositories
{
    public class UserRepositoryLocal : IUserRepository
    {
        private readonly List<User> _context;

        public UserRepositoryLocal()
        {
            _context = new List<User>();
        }
        public IEnumerable<User> GetAllUsers()
        {
            return _context.ToList();
        }
        
        public bool TryCreateUser(User user)
        {

            if(user.Id != Guid.Empty)
            {

            _context.Add(user);
            return true;

            }
            
            return false;
        }

        public User GetUserById(Guid id)
        {
            return _context.FirstOrDefault(u => u.Id == id);
        }

        public bool SaveChange()
        {
            return true;
        }
    }
}