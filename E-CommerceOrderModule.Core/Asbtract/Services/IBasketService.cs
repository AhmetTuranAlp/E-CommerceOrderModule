using E_CommerceOrderModule.Common;
using E_CommerceOrderModule.Core.DTOs;
using E_CommerceOrderModule.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceOrderModule.Core.Asbtract.Services
{
    public interface IBasketService : IService<Basket>
    {
        Task<Result<List<BasketDTO>>> GetAllBasketAsync();

        Task<Result<BasketDTO>> GetBasket(int userId, string productCode);

        Task<Result<bool>> UpdateBasket(BasketDTO basket);
        Task<Result<bool>> CreateBasket(BasketDTO basket);
        Task<Result<bool>> DeleteBasket(BasketDTO basket);
    }
}
