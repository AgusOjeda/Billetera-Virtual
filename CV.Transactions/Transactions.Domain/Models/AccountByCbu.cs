using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transactions.Domain.Models
{
    public class AccountByCbu
    {
        public Guid AccountId { get; set; }
        public decimal Balance { get; set; }
        public string Alias { get; set; }
        public string NameCurrency { get; set; }
        public string LongNameCurrency { get; set; }
        public string Cbu { get; set; }
    }
}
