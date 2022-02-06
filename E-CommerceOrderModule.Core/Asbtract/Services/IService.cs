using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceOrderModule.Core.Asbtract.Services
{
    public interface IService<T> where T : class
    {
        Task<T> GetByIdAsync(string id);

        Task<IQueryable<T>> GetAllAsync();

        Task<T> CreateAsync(T entity);

        Task<bool> RemoveAsync(T entity);

        Task<bool> UpdateAsync(T entity);
    }
}
