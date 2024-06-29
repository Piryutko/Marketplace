using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using EnsureThat;
using System.Text.RegularExpressions;

namespace UserRegistrationService.Models
{
    public sealed class User
    {

        public User(string name, string surname, string nickname, DateTime datebirth, string email)
        {
            try
            {
            Ensure.That(name).IsNotNullOrWhiteSpace();
            Ensure.String.Matches(name, @"^[a-zA-Z]+$");

            Ensure.That(surname).IsNotNullOrWhiteSpace();
            Ensure.String.Matches(surname, @"^[a-zA-Z]+$");

            Ensure.That(nickname).IsNotNullOrWhiteSpace();
            Ensure.String.Matches(nickname, @"^[a-zA-Z]+$");


            Ensure.That(datebirth).IsNotDefault();
            ValidateBirthDate(datebirth);

            Ensure.That(email).IsNotNullOrWhiteSpace();
            Ensure.That(email).Matches(@"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");

            }
            catch (Exception ex)
            {   
            Id = Guid.Empty;
            Name = String.Empty;
            Surname = String.Empty;
            Nickname = String.Empty;
            Datebirth = default;
            Email = String.Empty;
            Console.WriteLine($"Exception: {ex.Message}");

            return;
            }
            
            Id = Guid.NewGuid();
            Name = name;
            Surname = surname;
            Nickname = nickname;
            Datebirth = datebirth;
            Email = email;
        }
        
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string Nickname { get; set; }

        public DateTime Datebirth { get; set; }

        public string Email { get; set; }


        private void ValidateBirthDate(DateTime datebirth)
        {
            int currentYear = DateTime.Now.Year;
            int birthYear = datebirth.Year;
            int age = currentYear - birthYear;

            if (datebirth.Month > DateTime.Now.Month || (datebirth.Month == DateTime.Now.Month && datebirth.Day > DateTime.Now.Day))
            {
                age--;
            }

            if(age <= 100)
            {
                return;
            }

            throw new ArgumentException();
        }
    }
}