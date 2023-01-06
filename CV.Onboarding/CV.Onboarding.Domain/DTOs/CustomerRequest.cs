using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CV.Onboarding.Domain.DTOs
{    public class CustomerRequest
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Dni { get; set; }
        public string? Cuil { get; set; }
        public string? Phone { get; set; }
    }
}
