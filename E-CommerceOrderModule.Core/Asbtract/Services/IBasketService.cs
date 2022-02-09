using E_CommerceOrderModule.Common;
using E_CommerceOrderModule.Core.DTOs;
using E_CommerceOrderModule.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceOrderModule.Core.Asbtract.Services
{
    public interface IBasketService : IService<Basket>
    {
        Task<Result<List<BasketDTO>>> GetAllInBasketAsync(string userId);
        Task<Result<List<BasketDTO>>> GetAllSaleAsync(string userId, string basketId);
        Task<Result<List<BasketDTO>>> GetAllActiveBasketAsync(string userId);
        Task<Result<BasketDTO>> GetBasketProduct(string userId, string productCode);
        Task<Result<bool>> UpdateBasket(BasketDTO basket);
        Task<Result<bool>> CreateBasket(BasketDTO basket);
        Task<Result<bool>> DeleteBasket(BasketDTO basket);
    }
}
