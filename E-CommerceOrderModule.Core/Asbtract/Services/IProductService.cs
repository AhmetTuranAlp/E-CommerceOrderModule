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
    public interface IProductService : IService<Product>
    {
        Task<Result<List<ProductDTO>>> GetAllProductAsync();

        Task<Result<ProductDTO>> GetProductAsync(string productId);
        Task<Result<bool>> UpdateProduct(ProductDTO product);
    }
}
