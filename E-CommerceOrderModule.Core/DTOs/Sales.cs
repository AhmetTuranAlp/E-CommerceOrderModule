using E_CommerceOrderModule.Core.DTOs.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceOrderModule.Core.DTOs
{
    public class SalesDTO : BaseEntityDTO
    {
        public SalesDTO()
        {
            TotalPrice = 0;

        }

        private decimal _totalPrice;
        public decimal TotalPrice
        {
            get { return _totalPrice; }
            set { _totalPrice = Math.Round(value, 2); }
        }

        public string PaymentType { get; set; }

        public string UserName { get; set; }

        public string UserCode { get; set; }

        public int TotalQuantity { get; set; }

    }
}
