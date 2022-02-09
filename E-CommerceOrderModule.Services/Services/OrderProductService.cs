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
    public class OrderProductService : Service<OrderProduct>, IOrderProductService
    {
        private readonly IOrderProductRepository _orderProductRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public OrderProductService(IGenericRepository<OrderProduct> genericRepository, IUnitOfWork unitOfWork, IMapper mapper, IOrderProductRepository orderProductRepository) : base(genericRepository, unitOfWork)
        {
            _orderProductRepository = orderProductRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<bool>> CreateOrderProduct(OrderProductDTO orderProduct)
        {
            Result<bool> result = new Result<bool>();
            try
            {
                orderProduct.Status = orderProduct.Status;
                orderProduct.UpdateDate = DateTime.Now;
                orderProduct.UploadDate = DateTime.Now;

                OrderProduct entity = null;
                entity = _mapper.Map<OrderProduct>(orderProduct);
                await _orderProductRepository.CreateAsync(entity);
                await _unitOfWork.CommitAsync();
                result.ResultObject = true;
                result.SetTrue();
            }
            catch (Exception)
            {
                result.SetFalse();
                result.ResultMessage = StaticValue._defaultErrorMessage;
            }
            return result;
        }
    }
}
