using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserStorageService.Models
{
    public class User
    {
        // public User(string name, string surname, string nickname, DateTime datebirth, string email, Guid id)
        // {
        //     Name = name;
        //     Surname = surname;
        //     Nickname = nickname;
        //     Datebirth = datebirth;
        //     Email = email;
        //     Id = id;
        // }

        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string Nickname { get; set;}

        public DateTime Datebirth { get;set; }

        public string Email { get; set; }
    }
}