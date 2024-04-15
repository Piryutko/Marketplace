using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserRegistrationService.Enums;

namespace UserRegistrationService.Dtos
{
    public class UserPublishedDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string Nickname { get; set; }

        public DateTime Datebirth { get; set; }

        public string Email { get; set; }

        public UserEvents Event { get; set; }
    }
}