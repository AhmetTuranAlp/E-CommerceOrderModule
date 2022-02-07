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

    public class BasketService : Service<Basket>, IBasketService
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public BasketService(IGenericRepository<Basket> genericRepository, IUnitOfWork unitOfWork, IMapper mapper, IBasketRepository basketRepository) : base(genericRepository, unitOfWork)
        {
            _basketRepository = basketRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<List<BasketDTO>>> GetAllBasketAsync()
        {
            Result<List<BasketDTO>> result = new Result<List<BasketDTO>>();
            try
            {
                var baskets = await _basketRepository.GetAllAsync();
                if (baskets.ToList().Count > 0)
                {
                    var basketList = baskets.ToList().Where(x => x.Status != ModelEnums.Status.Deleted);
                    result.ResultObject = _mapper.Map<List<BasketDTO>>(basketList.ToList());
                    result.SetTrue();
                }
                else
                    result.SetFalse();

                return result;
            }
            catch (Exception)
            {
                result.SetFalse();
                result.ResultMessage = StaticValue._defaultErrorMessage;
            }
            return result;
        }

        public async Task<Result<BasketDTO>> GetBasket(int userId, string productCode)
        {
            Result<BasketDTO> result = new Result<BasketDTO>();
            try
            {
                var basketList = await _basketRepository.GetAllAsync();
                var basket = basketList.Where(x => x.Status != ModelEnums.Status.Deleted && x.UserCode == userId.ToString() && x.ProductCode == productCode).FirstOrDefault();
                if (basket != null)
                {
                    result.ResultObject = _mapper.Map<BasketDTO>(basket);
                    result.SetTrue();
                }
                else
                    result.SetFalse();
            }
            catch (Exception)
            {
                result.SetFalse();
                result.ResultMessage = StaticValue._defaultErrorMessage;
            }

            return result;
        }

        public async Task<Result<bool>> UpdateBasket(BasketDTO basket)
        {
            Result<bool> result = new Result<bool>();
            try
            {
                Basket entity = null;
                entity = _mapper.Map<Basket>(basket);
                _basketRepository.UpdateAsync(entity);



                //_basketRepository.RemoveAsync(entity);
                //await _unitOfWork.CommitAsync();
                //entity.Id = 0;
                //await _basketRepository.CreateAsync(entity);
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

        public async Task<Result<bool>> DeleteBasket(BasketDTO basket)
        {
            Result<bool> result = new Result<bool>();
            try
            {
                Basket entity = null;
                entity = _mapper.Map<Basket>(basket);
                _basketRepository.RemoveAsync(entity);
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

        public async Task<Result<bool>> CreateBasket(BasketDTO basket)
        {
            Result<bool> result = new Result<bool>();
            try
            {
                basket.Status = ModelEnumsDTO.Status.Active;
                basket.UpdateDate = DateTime.Now;
                basket.UploadDate = DateTime.Now;

                Basket entity = null;
                entity = _mapper.Map<Basket>(basket);
                await _basketRepository.CreateAsync(entity);
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
