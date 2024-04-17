using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Net.Client;
using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using UserRegistrationService.Interfaces;

namespace UserRegistrationService.GrpcClient
{
    public class UserClient : IUserClient
    {
        private readonly IConfiguration _configuration;

        public UserClient(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public bool GetResultRequestById(string id, out string result)
        {
            var isConnection = TryConnectionServer(out GrpcUserService.GrpcUserServiceClient client);

            if (isConnection)
            {
                var request = new UserRequest() {Id = id};

                var userTest = client.UserExists(request);

                result = userTest.Result;
                return true;
            }
            else
            {
                result = false.ToString();
                return false;
            }
        }

        private bool TryConnectionServer(out GrpcUserService.GrpcUserServiceClient client)
        {
            try
            {
            var channel = GrpcChannel.ForAddress(_configuration["GrpcUserServer"]);
            client = new GrpcUserService.GrpcUserServiceClient(channel);  
            return true;
                
            }
            catch (Exception ex)
            {
                 Console.WriteLine($"Error - {ex.Message}");
                 client = null;
                 return false;
            }
        }
    }
}