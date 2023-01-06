using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace CV.Onboarding.Domain.Entities
{
    public class Customer
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Dni { get; set; }
        public string Cuil { get; set; }
        public string Phone { get; set; }
        public IdentityVerification IdentityVerification { get; set; }
        public Address Address { get; set; }
    }
}
