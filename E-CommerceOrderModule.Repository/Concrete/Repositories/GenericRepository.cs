using E_CommerceOrderModule.Core.Asbtract.Repositories;
using E_CommerceOrderModule.Repository.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceOrderModule.Repository.Concrete.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly ECommerceOrderModuleContext _context;
        private readonly DbSet<T> _dbSet;

        public GenericRepository(ECommerceOrderModuleContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task CreateAsync(T entity)
        {   
            await _dbSet.AddAsync(entity);
        }

        public async Task<IQueryable<T>> GetAllAsync()
        {
            return _dbSet.AsNoTracking().AsQueryable();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public void RemoveAsync(T entity)
        {
            _dbSet.Remove(entity);
        }

        public void UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
        }
    }
}
