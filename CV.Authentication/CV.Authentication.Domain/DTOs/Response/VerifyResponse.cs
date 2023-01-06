using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CV.Authentication.Domain.DTOs.Response
{
    public class VerifyResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
    }
}
