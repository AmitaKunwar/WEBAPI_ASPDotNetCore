using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HomeAPI.Models
{
    public class ShoppingItemByCategoryName
    {
        [Key]
        public int CatID { get; set; }
        public string CategoryName { get; set; }
        public decimal ItemPrice { get; set; }

        public string UserID { get; set; }
        public string Username { get; set; }
    }
}
