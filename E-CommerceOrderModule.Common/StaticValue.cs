using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceOrderModule.Common
{
    public static class StaticValue
    {
        public static short _defaultSuccessCode = 100;
        public static string _defaultSuccessMessage = "İşleminiz başarılı bir şekilde tamamlanmıştır.";
        public static short _defaultErrorCode = 200;
        public static string _defaultErrorMessage = "Üzgünüz, beklenmedik bir hata oluştu. ";
        public static string _sqlServerConnectionString = "Data Source=LAPTOP-HU6ADIPN;Initial Catalog=ECOrderModuleDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
    }
}
