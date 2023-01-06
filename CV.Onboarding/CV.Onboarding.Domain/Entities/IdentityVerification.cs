using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CV.Onboarding.Domain.Entities
{
    public class IdentityVerification
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public int State { get; set; }
        public string IdentityInformation { get; set; }
        public VerificationState VerificationState { get; set; }
        public Customer Customer { get; set; }
    }
}
