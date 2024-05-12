using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Net.Client;
using Microsoft.Extensions.Configuration;
using ShopService.Interfaces;
using UserStorageService;

namespace ShopService.GrpcShopClient
{
    public class ShopClient : IShopClient
    {
        private readonly IConfiguration _configuration;

        public ShopClient(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        public bool GetResultRequestByNickname(string nickname, out string result)
        {
            var isConnection = TryConnectionServer(out GrpcUserService.GrpcUserServiceClient client);

            if (isConnection)
            {
                var request = new ShopRequest() {Nickname = nickname};

                var userTest = client.CheckingUserByNickname(request);

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
            var channel = GrpcChannel.ForAddress(_configuration["GrpcStorageServer"]);
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