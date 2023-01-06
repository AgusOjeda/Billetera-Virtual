using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CV.Onboarding.Domain.Entities
{
    public class Address
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public string Street { get; set; }
        public int Number { get; set; }
        public string Location { get; set; }
        public string Province { get; set; }
        public Customer Customer { get; set; }
    }
}
