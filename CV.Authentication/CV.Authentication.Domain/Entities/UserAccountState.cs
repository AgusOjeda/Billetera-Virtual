using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CV.Authentication.Domain.Entities
{
    public class UserAccountState
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public ICollection<User> Users { get; set; }
    }
}
