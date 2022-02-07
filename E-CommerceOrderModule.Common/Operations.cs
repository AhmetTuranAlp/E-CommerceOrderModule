using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceOrderModule.Common
{
    public class Operations
    {
        public static string UniqueRandom(int minInclusive, int maxInclusive, int keyLength)
        {
            string randomKey = string.Empty;
            Random rnd = new Random();

            for (int i = 0; i < keyLength; i++)
            {
                randomKey += rnd.Next(minInclusive, maxInclusive);
            }
            return randomKey;
        }
    }
}
