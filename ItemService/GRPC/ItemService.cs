using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Core;
using ItemService.Enums;
using ItemService.Interfaces;
using ItemService.Models;

namespace ItemService.GRPC
{
    public class ItemService : GrpcUserService.GrpcUserServiceBase
    {
        private readonly IItemRepository _itemRepository;

        public ItemService(IItemRepository itemRepository)
        {
            _itemRepository = itemRepository;
        }

        public override Task<GetItemsByCategoryResponse> GetItemsByCategory(GetItemsByCategoryRequest request,
         ServerCallContext context)
        {
            var result = _itemRepository.GetItemsByCategory((Category)Enum.ToObject(typeof(Category), request.CategoryId));

            var items = new GetItemsByCategoryResponse();

            var grpcItemsModels = new List<GrpcItemModels>();

            foreach (var item in result)
            {
                var targetItem = new GrpcItemModels()
                { Name = item.Name, Id = item.ToString(), 
                Category =(int)item.Category,
                Cost = (double)item.Cost, 
                Quantity = item.Quantity };
                
                grpcItemsModels.Add(targetItem);
            }

            items.Items.AddRange(grpcItemsModels);

            return Task.FromResult(items);
        }


    }
}