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
                Name = "Iphone 11 PRO",
                ProductId = "P100AIPRO",
                MarketPrice = 100,
                SalePrice = 100,
                Stock = 100,
                BrandName = "Apple",
                CategoryName = "Cep Telefonu",
                CurrencyType = CurrencyType.TRY,
                ShortDescription = "P100AIPRO Apple Iphone 11 PRO",
                Description = "P100AIPRO Iphone 11 PRO",
                Image = "https://mdbcdn.b-cdn.net/img/Photos/new-templates/bootstrap-shopping-carts/img1.webp",
                KDV = 1,
                UploadDate = DateTime.Now
            });

            builder.HasData(new Product()
            {
                Id = 2,
                Status = Status.NewRecord,
                Name = "Samsung Galaxy Note 10",
                ProductId = "P200SGN10",
                MarketPrice = 200,
                SalePrice = 200,
                Stock = 100,
                BrandName = "Samsung",
                CategoryName = "Cep Telefonu",
                CurrencyType = CurrencyType.TRY,
                ShortDescription = "P200SGN10 Samsung Galaxy Note 10",
                Description = "P200SGN10 Samsung Galaxy Note 10",
                Image = "https://mdbcdn.b-cdn.net/img/Photos/new-templates/bootstrap-shopping-carts/img2.webp",
                KDV = 1,
                UploadDate = DateTime.Now
            });
            builder.HasData(new Product()
            {
                Id = 3,
                Status = Status.NewRecord,
                Name = "Canon EOS M50",
                ProductId = "P300CEM",
                MarketPrice = 300,
                SalePrice = 300,
                Stock = 100,
                BrandName = "Canon",
                CategoryName = "Kamera",
                CurrencyType = CurrencyType.TRY,
                ShortDescription = "P300CEM Canon EOS M50",
                Description = "P300CEM Canon EOS M50",
                Image = "https://mdbcdn.b-cdn.net/img/Photos/new-templates/bootstrap-shopping-carts/img3.webp",
                KDV = 1,
                UploadDate = DateTime.Now
            });
            builder.HasData(new Product()
            {
                Id = 4,
                Status = Status.NewRecord,
                Name = "MacBook Pro",
                ProductId = "P300MBPRO",
                MarketPrice = 400,
                SalePrice = 400,
                Stock = 100,
                BrandName = "Apple",
                CategoryName = "Bilgisayar",
                CurrencyType = CurrencyType.TRY,
                ShortDescription = "P300MBPRO MacBook Pro",
                Description = "P300MBPRO MacBook Pro",
                Image = "https://mdbcdn.b-cdn.net/img/Photos/new-templates/bootstrap-shopping-carts/img4.webp",
                KDV = 1,
                UploadDate = DateTime.Now
            });
        }
    }
}
