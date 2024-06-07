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

        public override Task<CheckQuantityItemResponse> CheckQuantityItem(CheckQuantityItemRequest request,
        ServerCallContext context)
        {
            var value = Guid.TryParse(request.ItemsId, out var itemsId);

            if(value == true)
            {

            var result = _itemRepository.CheckQuantityItem(itemsId, request.Quantity);

            var cost = _itemRepository.GetCostItem(itemsId);

            var itemName = _itemRepository.GetItemByName(itemsId);

            var response = new CheckQuantityItemResponse(){Result = result.ToString(), Cost = cost.ToString(), ItemName = itemName};

            return Task.FromResult(response); 

            }

            var badResponse = new CheckQuantityItemResponse(){Result = false.ToString(), Cost = _MINIMALVALUE.ToString(), ItemName = default};

            return Task.FromResult(badResponse); 

        }

        public override Task<TryAddItemInShoppCartResponse> TryAddItemInShoppCart(TryAddItemInShoppCartRequest request,
        ServerCallContext context)
        {
            var value = Guid.TryParse(request.ItemsId, out var itemsId);

            if(value == true)
            {

            var result = _itemRepository.CheckQuantityById(itemsId, request.Quantity);

            var cost = _itemRepository.GetCostItem(itemsId);

            var itemName = _itemRepository.GetItemByName(itemsId);

            var response = new TryAddItemInShoppCartResponse(){Result = result.ToString(), Cost = cost.ToString(), ItemName = itemName, Id = request.ItemsId.ToString()};

            return Task.FromResult(response); 

            }

            var badResponse = new TryAddItemInShoppCartResponse(){Result = false.ToString(), Cost = _MINIMALVALUE.ToString(), ItemName = default, Id = default};

            return Task.FromResult(badResponse); 

        }
        

        
    }
}