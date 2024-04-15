using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserStorageService.Enums;
using UserStorageService.Models;

namespace UserStorageService.Dtos
{
    public class GenericEventDto
    {
        public UserEvents Event { get; set; }
    }
}