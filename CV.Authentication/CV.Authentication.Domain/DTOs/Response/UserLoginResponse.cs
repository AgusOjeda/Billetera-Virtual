using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CV.Authentication.Domain.DTOs.Response
{
    public class UserLoginResponse
    {
        public string Email { get; set; }
        public string Token { get; set; }
    }
    public class UserResponse
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
        public StateDto State { get; set; }
    }
}
