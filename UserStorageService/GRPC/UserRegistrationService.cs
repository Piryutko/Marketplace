using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Core;
using UserStorageService.Interfaces;
using UserStorageService.Models;

namespace UserStorageService.GRPC
{
    public class UserRegistrationService : GrpcUserService.GrpcUserServiceBase
    {
        private readonly IUserRepository _repository;

        public UserRegistrationService(IUserRepository repository)
        {
            _repository = repository;
        }

        public override Task<UserResponse> UserExists(UserRequest request, ServerCallContext context)
        {
            var id = Guid.Parse(request.Id);

            var result = _repository.UserExists(id);
            
            var response = new UserResponse(){Result = result.ToString()};

            return Task.FromResult(response);
        }

        public override Task<ShopResponse> CheckingUserByNickname(ShopRequest request, ServerCallContext context)
        {
            var result = _repository.CheckingUserByNickname(request.Nickname);
            
            var response = new ShopResponse(){Result = result.ToString()};

            return Task.FromResult(response);
        }

    }
}