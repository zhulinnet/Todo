using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace ToDo.Infrastructure.Extensions
{
    public static class StringExtensions
    {
        public static string ToMd5Caps16(this String str)
        {
            using (var md5 = MD5.Create())
            {
                var data = md5.ComputeHash(Encoding.UTF8.GetBytes(str));
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < data.Length; i++)
                {
                    builder.Append(data[i].ToString("X2"));
                }
                string result4 = builder.ToString().Substring(8, 16);
                return result4;
            }
        }
    }
}
