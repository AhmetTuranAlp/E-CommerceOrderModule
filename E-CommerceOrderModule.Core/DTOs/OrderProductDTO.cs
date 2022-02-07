using E_CommerceOrderModule.Core.DTOs.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceOrderModule.Core.DTOs
{
    public class OrderProductDTO : BaseEntityDTO
    {
        public string OrderNumber { get; set; }
        public string Name { get; set; }
        public string ProductId { get; set; }


        private decimal _marketPrice;

        public decimal MarketPrice
        {
            get { return _marketPrice; }
            set { _marketPrice = Math.Round(value, 2); }
        }

        private decimal _salePrice;

        public decimal SalePrice
        {
            get { return _salePrice; }
            set { _salePrice = Math.Round(value, 2); }
        }
    }
}
