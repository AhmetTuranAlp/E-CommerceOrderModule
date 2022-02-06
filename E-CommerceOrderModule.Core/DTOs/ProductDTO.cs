using E_CommerceOrderModule.Core.DTOs.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static E_CommerceOrderModule.Core.DTOs.ModelEnumsDTO;

namespace E_CommerceOrderModule.Core.DTOs
{
    public class ProductDTO : BaseEntityDTO
    {
        public string Name { get; set; }
        public string ProductId { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public string BrandName { get; set; }
        public string CategoryName { get; set; }
        public string Image { get; set; }


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

        public decimal KDV { get; set; }
        public CurrencyType CurrencyType { get; set; }

        public int Stock { get; set; }
    }
}
