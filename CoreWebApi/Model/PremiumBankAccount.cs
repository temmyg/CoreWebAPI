using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreWebApi.Model
{
    public class PremiumBankAccount : IBankAccount
    {
        public string AccountNo { get; set; }
    }
}
