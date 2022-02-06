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
 
    public class UserService : Service<User>, IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public UserService(IGenericRepository<User> genericRepository, IUnitOfWork unitOfWork, IMapper mapper, IUserRepository userRepository) : base(genericRepository, unitOfWork)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<Result<UserDTO>> GetUserAsync()
        {
            Result<UserDTO> result = new Result<UserDTO>();
            UserDTO user = new UserDTO()
            {
                Email = "ahmetturanalp@gmail.com",
                Id = 1,
                Password = "123123",
                UserName = "Ahmet Turan Alp"
            };
            result.ResultObject = user;
            result.SetTrue();

            return result;
        }
    }
}
