using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreWebApi.Model
{
    public class FillingItem
    {
        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public int Number { get; set; }
        public string Description { set; get; }

        public int VendorId { get; set; }
    }
}
