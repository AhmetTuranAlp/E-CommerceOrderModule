using E_CommerceOrderModule.Core.Asbtract.Repositories;
using E_CommerceOrderModule.Core.Entity;
using E_CommerceOrderModule.Repository.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceOrderModule.Repository.Concrete.Repositories
{

    public class SaleRepository : GenericRepository<Sales>, ISaleRepository
    {
        public SaleRepository(ECommerceOrderModuleContext context) : base(context)
        {

        }


    }
}
