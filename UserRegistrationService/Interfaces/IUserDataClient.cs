using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserRegistrationService.Interfaces
{
    public interface IUserDataClient
    {
        bool ExistsUser(Guid id);
    }
}