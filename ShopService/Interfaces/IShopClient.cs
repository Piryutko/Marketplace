using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopService.Interfaces
{
    public interface IShopClient
    {
        public bool GetResultRequestByNickname(string nickname, out string result);
    }
}