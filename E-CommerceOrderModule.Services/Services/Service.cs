using AutoMapper;
using E_CommerceOrderModule.Common;
using E_CommerceOrderModule.Core.Asbtract;
using E_CommerceOrderModule.Core.Asbtract.Repositories;
using E_CommerceOrderModule.Core.Asbtract.Services;
using E_CommerceOrderModule.Core.DTOs;
using E_CommerceOrderModule.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceOrderModule.Services.Services
{
    public class Service<T> : IService<T> where T : class
    {
        private readonly IGenericRepository<T> _genericRepository;
        private readonly IUnitOfWork _unitOfWork;
    
        public Service(IGenericRepository<T> genericRepository, IUnitOfWork unitOfWork)
        {
            _genericRepository = genericRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<T> CreateAsync(T entity)
        {
            await _genericRepository.CreateAsync(entity);
            await _unitOfWork.CommitAsync();
            return entity;
        }

        public Task<IQueryable<T>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<T> GetByIdAsync(string id)
        {
            return await GetByIdAsync(id);
        }

        public async Task<bool> RemoveAsync(T entity)
        {
            _genericRepository.RemoveAsync(entity);
            await _unitOfWork.CommitAsync();
            return true;
        }

        public async Task<bool> UpdateAsync(T entity)
        {
            _genericRepository.UpdateAsync(entity);
            await _unitOfWork.CommitAsync();
            return true;
        }

    }
}
