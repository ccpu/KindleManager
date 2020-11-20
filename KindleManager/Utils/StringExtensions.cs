using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KindleManager.Utils
{
    public static class StringExtensions
    {
        public static string ToUTF8(this string text)
        {
            byte[] bytes = Encoding.Default.GetBytes(text);
            return Encoding.UTF8.GetString(bytes);
        }
    }
}
