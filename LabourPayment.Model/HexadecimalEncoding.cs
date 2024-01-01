using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabourPayment.Model
{
    public static class HexadecimalEncoding
    {
        public static string ToHexString(string str)
        {
            var sb = new StringBuilder();
            var bytes = Encoding.Unicode.GetBytes(str);
            foreach(var t in bytes)
            {
                sb.Append(t.ToString("X2"));
            }
            return sb.ToString();   
        }

    }
}
