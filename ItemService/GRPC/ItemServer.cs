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
    public class ItemServer : GrpcUserService.GrpcUserServiceBase
    {
        private readonly IItemRepository _itemRepository;
        private readonly IMapper _mapper;

        public ItemServer (IItemRepository itemRepository, IMapper mapper)
        {
            _itemRepository = itemRepository;
            _mapper = mapper;

        }

        const int _MINIMALVALUE = 0;

        public override Task<GetItemsByCategoryResponse> GetItemsByCategory(GetItemsByCategoryRequest request,
         ServerCallContext context)
        {
            var result = _itemRepository.GetItemsByCategory((Category)Enum.ToObject(typeof(Category), request.CategoryId));

            var items = new GetItemsByCategoryResponse();

            foreach (var item in result)
            {
                items.Items.Add(_mapper.Map<GrpcItemModels>(item));
            }

            return Task.FromResult(items);
        }

        public override Task<GetItemsByCategoryResponse> GetItemsCategorySortByCost(GetItemsByCategoryRequest request,
         ServerCallContext context)
        {
            var result = _itemRepository.GetItemsCategorySortByCost((Category)Enum.ToObject(typeof(Category), request.CategoryId));

            var items = new GetItemsByCategoryResponse();

            foreach (var item in result)
            {
                items.Items.Add(_mapper.Map<GrpcItemModels>(item));
            }

            return Task.FromResult(items);
        }

        public override Task<GetItemsByCategoryResponse> GetItemsCategorySortByCostDescending(GetItemsByCategoryRequest request,
         ServerCallContext context)
        {
            var result = _itemRepository.GetItemsCategorySortByCostDescending((Category)Enum.ToObject(typeof(Category), request.CategoryId));

            var items = new GetItemsByCategoryResponse();

            foreach (var item in result)
            {
                items.Items.Add(_mapper.Map<GrpcItemModels>(item));
            }

            return Task.FromResult(items);
        }

        public override Task<BuyItemsResponse> BuyItems(BuyItemsRequest request,
        ServerCallContext context)
        {
            var value = Guid.TryParse(request.ItemsId, out var itemsId);

            if(value == true)
            {

            var result = _itemRepository.BuyItem(itemsId, request.Quantity);

            var cost = _itemRepository.GetCostItem(itemsId);

            var response = new BuyItemsResponse(){Result = result.ToString(), Cost = cost.ToString()};

            return Task.FromResult(response); 

            }

            var badResponse = new BuyItemsResponse(){Result = false.ToString(), Cost = _MINIMALVALUE.ToString()};

            return Task.FromResult(badResponse); 

        }
        

        
    }
}