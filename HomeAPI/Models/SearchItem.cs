using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeAPI.Models
{
    public class SearchItem
    {
        public string CategoryName { get; set; }
        public string ItemName { get; set; }
        public decimal ItemPrice_1 { get; set; }
        public decimal ItemPrice_2 { get; set; }
        public string UserID { get; set; }

    }
}
