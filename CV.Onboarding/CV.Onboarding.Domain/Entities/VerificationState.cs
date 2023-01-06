using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CV.Onboarding.Domain.Entities
{
    public class VerificationState
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<IdentityVerification> IdentityVerifications { get; set; }
    }
}
