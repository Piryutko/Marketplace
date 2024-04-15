using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserRegistrationService.Data;
using UserRegistrationService.Interfaces;
using UserRegistrationService.Models;

namespace UserRegistrationService.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }
        public IEnumerable<User> GetAllUsers()
        {
            return _context.Users.ToList();
        }
        
        public bool TryCreateUser(User user)
        {

            if(user.Id != Guid.Empty)
            {
            _context.Users.Add(user);
            return SaveChange();
            }
            
            return false;
        }

        public User GetUserById(Guid id)
        {
            return _context.Users.FirstOrDefault(u => u.Id == id);
        }

        public bool SaveChange()
        {
            return (_context.SaveChanges() >= 0);
        }
    }
}