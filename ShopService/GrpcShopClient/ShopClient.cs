using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using AutoMapper;
using Grpc.Net.Client;
using Microsoft.Extensions.Configuration;
using ShopService.Interfaces;
using ShopService.Models;
using UserStorageService;

namespace ShopService.GrpcShopClient
{
    public class ShopClient : IShopClient
    {
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public ShopClient(IConfiguration configuration, IMapper mapper)
        {
            _configuration = configuration;
            _mapper = mapper;
        }


        public bool GetResultRequestByNickname(string nickname, out string result)
        {
            var isConnection = TryConnectionUserStorageServer(out GrpcUserService.GrpcUserServiceClient client);

            if (isConnection)
            {
                var request = new ShopRequest() {Nickname = nickname};

                var targetUser = client.CheckingUserByNickname(request);

                result = targetUser.Result;
                return true;
            }
            else
            {
                result = false.ToString();
                return false;
            }
        }

        public IEnumerable<Item> GetItemsByCategory(int categoryId)
        {
            var isConnection = TryConnectionItemServer(out GrpcUserService.GrpcUserServiceClient client);

            if (isConnection)
            {
                var request = new GetItemsByCategoryRequest(){CategoryId = categoryId};

                var itemsResponse = client.GetItemsByCategory(request);

                var items = _mapper.Map<IEnumerable<Item>>(itemsResponse.Items);

                return items;
            }
            else
            {
                var testList = new List<Item>();
                return testList;
            }
        }

        public IEnumerable<Item> GetItemsCategorySortByCost(int categoryId)
        {
            var isConnection = TryConnectionItemServer(out GrpcUserService.GrpcUserServiceClient client);

            if (isConnection)
            {
                var request = new GetItemsByCategoryRequest(){CategoryId = categoryId};

                var itemsResponse = client.GetItemsCategorySortByCost(request);

                var items = _mapper.Map<IEnumerable<Item>>(itemsResponse.Items);

                return items;
            }
            else
            {
                var testList = new List<Item>();
                return testList;
            }
        }

        public IEnumerable<Item> GetItemsCategorySortByCostDescending(int categoryId)
        {
            var isConnection = TryConnectionItemServer(out GrpcUserService.GrpcUserServiceClient client);

            if (isConnection)
            {
                var request = new GetItemsByCategoryRequest(){CategoryId = categoryId};

                var itemsResponse = client.GetItemsCategorySortByCostDescending(request);

                var items = _mapper.Map<IEnumerable<Item>>(itemsResponse.Items);

                return items;
            }
            else
            {
                var result = new List<Item>();
                return result;
            }
        }

        public bool TryBuyItems(Guid id, int quantity, out decimal cost)
        {
            var isConnection = TryConnectionItemServer(out GrpcUserService.GrpcUserServiceClient client);

            if(isConnection)
            {
                var request = new BuyItemsRequest(){ItemsId = id.ToString(), Quantity = quantity};

                var result = client.BuyItems(request);

                bool.TryParse(result.Result, out bool response);

                if(response == true)
                {
                    cost = Decimal.Parse(result.Cost);
                    return response;
                }
                
            }
            cost = default;
            return false;
        }

        private bool TryConnectionUserStorageServer(out GrpcUserService.GrpcUserServiceClient client) //Объединить с методом TryConnectionItemServer
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

        private bool TryConnectionItemServer(out GrpcUserService.GrpcUserServiceClient client)
        {
            try
            {
            var channel = GrpcChannel.ForAddress(_configuration["GrpcItemServer"]);
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