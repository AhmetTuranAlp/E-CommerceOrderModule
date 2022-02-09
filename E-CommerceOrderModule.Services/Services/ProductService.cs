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

    public class ProductService : Service<Product>, IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public ProductService(IGenericRepository<Product> genericRepository, IUnitOfWork unitOfWork, IMapper mapper, IProductRepository productRepository) : base(genericRepository, unitOfWork)
        {
            _productRepository = productRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<List<ProductDTO>>> GetAllProductAsync()
        {
            Result<List<ProductDTO>> result = new Result<List<ProductDTO>>();
            var products = await _productRepository.GetAllAsync(x => x.Status == ModelEnums.Status.NewRecord);
            if (products.ToList().Count > 0)
            {
                result.ResultObject = _mapper.Map<List<ProductDTO>>(products.ToList());
                result.SetTrue();
            }
            else
                result.SetFalse();

            return result;
        }

        public async Task<Result<ProductDTO>> GetProductAsync(string productId)
        {
            Result<ProductDTO> result = new Result<ProductDTO>();
            var products = await _productRepository.GetAsync(x => x.Status == ModelEnums.Status.NewRecord && x.ProductId == productId);
            if (products != null)
            {
                result.ResultObject = _mapper.Map<ProductDTO>(products);
                result.SetTrue();
            }
            else
                result.SetFalse();

            return result;
        }

        public async Task<Result<bool>> UpdateProduct(ProductDTO product)
        {
            Result<bool> result = new Result<bool>();
            try
            {
                Product entity = null;
                entity = _mapper.Map<Product>(product);
                _productRepository.UpdateAsync(entity);
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
