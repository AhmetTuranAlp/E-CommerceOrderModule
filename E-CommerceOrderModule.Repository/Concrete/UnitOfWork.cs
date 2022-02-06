using E_CommerceOrderModule.Core.Asbtract;
using E_CommerceOrderModule.Repository.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceOrderModule.Repository.Concrete
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ECommerceOrderModuleContext _context;

        public UnitOfWork(ECommerceOrderModuleContext context)
        {
            _context = context;
        }

        public void Commit()
        {
            _context.SaveChanges();
        }

        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
