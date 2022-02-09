using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceOrderModule.Core.Asbtract.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        //Task<T> GetByIdAsync(int id);

        //Task<IQueryable<T>> GetAllAsync();

        Task CreateAsync(T entity);

        void RemoveAsync(T entity);

        void UpdateAsync(T entity);

        Task<IQueryable<T>> GetAllAsync(Expression<Func<T, bool>> filter = null);

        Task<T> GetAsync(Expression<Func<T, bool>> filter = null);
    }
}
