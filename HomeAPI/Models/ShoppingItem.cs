using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HomeAPI.Models
{
    public class ShoppingItem
    {
        [Key]
        public int Id { get; set; }
        public string itemName { get; set; }
        public decimal itemPrice { get; set; }
        public string UserID { get; set; }
        public string Username { get; set; }
    }
}
