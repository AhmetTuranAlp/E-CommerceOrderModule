using E_CommerceOrderModule.Core.Entity.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceOrderModule.Core.Entity
{
    public class Basket : BaseEntity
    {
        public string BasketId { get; set; }

        private decimal _price;
        public decimal Price
        {
            get { return _price; }
            set { _price = Math.Round(value, 2); }
        }
        public int Quantity { get; set; }
        public string Image { get; set; }
        public string ProductCode { get; set; }

        public string ProductName { get; set; }


        public string UserCode { get; set; }

    }
}
