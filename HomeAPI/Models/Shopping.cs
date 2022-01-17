using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HomeAPI.Models
{
    public class ShoppingList
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        public string Itemname { get; set; }
        public string ShoppingDescription { get; set; }
        public decimal ItemPrice { get; set; }
        public int CategoryID { get; set; }
        public string UserID { get; set; }
    }
}
