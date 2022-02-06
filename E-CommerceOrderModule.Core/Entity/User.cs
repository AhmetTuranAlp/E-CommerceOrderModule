using E_CommerceOrderModule.Core.Entity.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceOrderModule.Core.Entity
{
    public class User : BaseEntity
    {
        public User()
        {
            UserName = "";
            Password = "";
            Email = "";
        }
        [Required(ErrorMessage = "Zorunlu Alan")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Zorunlu Alan")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Zorunlu Alan")]
        public string Email { get; set; }
    }
}
