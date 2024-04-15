using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserRegistrationService.Dtos;

namespace UserRegistrationService.Interfaces
{
    public interface IMessageBusClient
    {
        void PublishNewUser(UserPublishedDto userPublishedDto);
    }
}