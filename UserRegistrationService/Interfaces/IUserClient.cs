using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserRegistrationService.Interfaces
{
    public interface IUserClient
    {
        public bool GetResultRequestById(string id, out string result);
    }
}