using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CV.Authentication.Application.Tools
{
    public class Tools
    {
        public static Guid CreateRandomToken()
        {
            return Guid.NewGuid();
        }
        public static string CreateCode()
        {
            Random rnd = new Random();
            int code = rnd.Next(10000000, 100000000);
            return code.ToString();
        }        
    }
}
