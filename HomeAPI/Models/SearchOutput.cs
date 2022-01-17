using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HomeAPI.Models
{
    public class SearchOutput
    {
        [Key]
        public int Id { get; set; }
        public string ItemName { get; set; }
        public string ShoppingDescription { get; set; }
        public decimal ItemPrice { get; set; }
        public string UserID { get; set; }

        public string UserName { get; set; }
    }
}
