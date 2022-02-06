using E_CommerceOrderModule.Core.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static E_CommerceOrderModule.Core.Entity.ModelEnums;

namespace E_CommerceOrderModule.Repository.Seeds
{
    public class ProductSeed : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasData(new Product()
            {
                Id = 1,
                Status = Status.NewRecord,
                Name = "P1",
                ProductId = "P100",
                MarketPrice = 100,
                SalePrice = 100,
                Stock = 10,
                BrandName = "B1",
                CategoryName = "C1",
                CurrencyType = CurrencyType.TRY,
                ShortDescription = "P1",
                Description = "P1",
                Image = "",
                KDV = 1,
                UploadDate = DateTime.Now
            });

            builder.HasData(new Product()
            {
                Id = 2,
                Status = Status.NewRecord,
                Name = "P2",
                ProductId = "P200",
                MarketPrice = 200,
                SalePrice = 200,
                Stock = 20,
                BrandName = "B2",
                CategoryName = "C2",
                CurrencyType = CurrencyType.TRY,
                ShortDescription = "P2",
                Description = "P2",
                Image = "",
                KDV = 1,
                UploadDate = DateTime.Now
            });
        }
    }
}
