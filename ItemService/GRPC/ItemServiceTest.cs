using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Grpc.Core;
using ItemService.Enums;
using ItemService.Interfaces;

namespace ItemService.GRPC
{
    public class ItemServiceTest : GrpcUserService.GrpcUserServiceBase
    {
        private readonly IItemRepository _itemRepository;
        private readonly IMapper _mapper;

        public ItemServiceTest(IItemRepository itemRepository, IMapper mapper)
        {
            _itemRepository = itemRepository;
            _mapper = mapper;

        }

        public override Task<GetItemsByCategoryResponse> GetItemsByCategory(GetItemsByCategoryRequest request,
         ServerCallContext context)
        {
            var result = _itemRepository.GetItemsByCategory((Category)Enum.ToObject(typeof(Category), request.CategoryId));

            var items = new GetItemsByCategoryResponse();

            // var grpcItemsModels = new List<GrpcItemModels>();

            // foreach (var item in result)
            // {
            //     var targetItem = new GrpcItemModels()
            //     { Name = item.Name, Id = item.ToString(), 
            //     Category =(int)item.Category,
            //     Cost = (double)item.Cost, 
            //     Quantity = item.Quantity };
                
            //     grpcItemsModels.Add(targetItem);
            // }

            // items.Items.AddRange(grpcItemsModels);

            foreach (var item in result)
            {
                items.Items.Add(_mapper.Map<GrpcItemModels>(item));
            }

            return Task.FromResult(items);
        }
    }
}