using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserStorageService.Data;
using UserStorageService.Interfaces;
using UserStorageService.Models;

namespace UserStorageService.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public bool AddUser(User user)
        {
            if(CheckingUserByNickname(user) && CheckingUserByEmail(user))
            {
                _context.Users.Add(user);
                return SaveChange();
            }
            return false;
        }

        public IEnumerable<User> GetAllUsers()
        {
            return _context.Users.ToList();
        }

        public bool UserExists(Guid Id)
        {
            var result = _context.Users.Any(u => u.Id == Id);
            return result;
        }
        
        public bool CheckingUserByNickname(User User)
        {
            if(GetAllUsers().Any(u => u.Nickname == User.Nickname))
            {
                return false;
            }

            return true;
        }

        public bool CheckingUserByEmail(User User)
        {
            if(GetAllUsers().Any(u => u.Email == User.Email))
            {
                return false;
            }

            return true;

        }

        public bool TryFindUserByNickName(string Nickname, out User User)
        {
            var result = GetAllUsers().FirstOrDefault(u => u.Nickname == Nickname);

            if(result == null)
            {
                User = null;
                return false;
            }

            User = result;
            return true;
        }

        public bool TryFindUserByEmail(string Email, out User User)
        {
            var result = GetAllUsers().FirstOrDefault(u => u.Email == Email);

            if(result == null)
            {
                User = null;
                return false;
            }

            User = result;
            return true;

        }

        public bool TryFindUserById(Guid Id, out User User)
        {
            var result = GetAllUsers().FirstOrDefault(u => u.Id == Id);

            if(result == null)
            {
                User = null;
                return false;
            }

            User = result;
            return true;
        }

        public bool TryDeleteUserById(Guid Id)
        {
            var result = TryFindUserById(Id, out User user);
            
            if(result)
            {
                _context.Users.Remove(user);
                SaveChange();
                return true;
            }

            return false;
        }

        public bool SaveChange()
        {
            return (_context.SaveChanges() >= 0);
        }
    }
}