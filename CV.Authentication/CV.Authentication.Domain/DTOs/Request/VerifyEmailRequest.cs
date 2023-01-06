using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CV.Authentication.Domain.DTOs.Request
{
    public class VerifyEmailRequest
    {
        public string Token { get; set; }
    }
}
