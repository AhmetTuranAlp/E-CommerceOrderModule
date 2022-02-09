using E_CommerceOrderModule.Core.Asbtract.Repositories;
using E_CommerceOrderModule.Repository.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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
        protected async virtual Task<IQueryable<T>> GetQueryable(Expression<Func<T, bool>> filter = null)
        {
            IQueryable<T> query = _dbSet.AsNoTracking().AsQueryable(); 

            if (filter != null)
            {
                query = query.Where(filter);
            }

            return query;
        }

        protected async virtual Task<T> Get(Expression<Func<T, bool>> filter = null)
        {
            IQueryable<T> query = _dbSet.AsNoTracking().AsQueryable();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            return query.FirstOrDefault();
        }

        protected async virtual Task<IQueryable<T>> GetQueryable(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, int? skip = null, int? take = null)
        {
            IQueryable<T> query = _dbSet.AsNoTracking().AsQueryable();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            if (skip.HasValue)
            {
                query = query.Skip(skip.Value);
            }

            if (take.HasValue)
            {
                query = query.Take(take.Value);
            }

            return query;
        }

        //public async Task<IQueryable<T>> GetAllAsync()
        //{
        //    return _dbSet.AsNoTracking().AsQueryable();
        //}

        //public async Task<T> GetByIdAsync(int id)
        //{
        //    return await _dbSet.FindAsync(id);
        //}

        public void RemoveAsync(T entity)
        {
            _dbSet.Remove(entity);
        }

        public void UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
        }

        public async Task<IQueryable<T>> GetAllAsync(Expression<Func<T, bool>> filter = null)
        {
            return await GetQueryable(filter);
        }
        public async Task<T> GetAsync(Expression<Func<T, bool>> filter = null)
        {
            return await Get(filter);
        }
    }
}
