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
    public class SaleService : Service<Sales>, ISaleService
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public SaleService(IGenericRepository<Sales> genericRepository, IUnitOfWork unitOfWork, IMapper mapper, ISaleRepository saleRepository) : base(genericRepository, unitOfWork)
        {
            _saleRepository = saleRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<bool>> CreateSales(SalesDTO sales)
        {
            Result<bool> result = new Result<bool>();
            try
            {

                Sales entity = null;
                entity =  _mapper.Map<Sales>(sales);
                await _saleRepository.CreateAsync(entity);
                await _unitOfWork.CommitAsync();
                result.ResultObject = true;
                result.SetTrue();
            }
            catch (Exception ex)
            {
                result.SetFalse();
                result.ResultMessage = StaticValue._defaultErrorMessage;
            }
            return result;
        }
    }
}
