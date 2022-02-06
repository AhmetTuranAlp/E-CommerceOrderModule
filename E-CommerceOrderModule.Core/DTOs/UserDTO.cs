using E_CommerceOrderModule.Core.DTOs.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceOrderModule.Core.DTOs
{
    public class UserDTO : BaseEntityDTO
    {
        public UserDTO()
        {
            UserName = "";
            Password = "";
            Email = "";
        }

        public string UserName { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }
    }
}
