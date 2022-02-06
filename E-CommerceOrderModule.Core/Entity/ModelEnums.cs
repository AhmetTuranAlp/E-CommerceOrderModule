using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceOrderModule.Core.Entity
{
    public class ModelEnums
    {
        public enum Status : int
        {
            [Description("Silindi")]
            Deleted = 0,
            [Description("Aktif")]
            Active = 1,
            [Description("Pasif")]
            Passive = 2,
            [Description("Yeni Kayıt")]
            NewRecord = 3
        }

        public enum PaymentType : int
        {
            [Description("Kredi Kartı Online Ödeme")]
            CreditCardOnline = 0,
            [Description("Kredi Kartı Kapıda Ödeme")]
            CreditCardAtTheDoor = 1,
            [Description("Kapıda Nakit Ödeme")]
            CashAtTheDoor = 2
        }

        public enum ProductType
        {
            [Description("Sıvı Yağlar")]
            LiquidOils = 0,
            [Description("Organik Gıda")]
            OrganicFood = 1,
            [Description("Gurme Ürünler")]
            GourmetProducts = 2,
            [Description("Glutensiz Ürünler")]
            GlutenFreeProducts = 3,
            [Description("Kuruyemiş")]
            Nuts = 4,
            [Description("Baharat")]
            Spice = 5,
            [Description("Aktar Ürünleri")]
            TransferProducts = 6,
            [Description("Kuru Gıda")]
            DryFood = 7,
            [Description("Kahvaltılık")]
            ForBreakfast = 8,
            [Description("Atıştırmalıklar")]
            Snacks = 9,
            [Description("Şarküteri")]
            Delicatessen = 10,
            [Description("Yemek Sosları")]
            CookingSauces = 11,
            [Description("Hediyelik Çikolata")]
            GiftChocolate = 12,
            [Description("Manav")]
            Greengrocer = 13,
        }


        public enum CurrencyType
        {
            [Description("TL")]
            TRY = 0,
            [Description("USD")]
            USD = 1,
            [Description("EURO")]
            EURO = 2,
        }
    }
}
