using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HomeAPI.Models
{
    public class ShoppingCategory
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int CatID { get; set; }
        public string CategoryName { get; set; }      
    }
}
