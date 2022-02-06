using E_CommerceOrderModule.Core.Asbtract.Repositories;
using E_CommerceOrderModule.Core.Entity;
using E_CommerceOrderModule.Repository.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceOrderModule.Repository.Concrete.Repositories
{
    public class ProductRespository : GenericRepository<Product>, IProductRepository
    {
        public ProductRespository(ECommerceOrderModuleContext context) : base(context)
        {
        
        }

     
    }
}
