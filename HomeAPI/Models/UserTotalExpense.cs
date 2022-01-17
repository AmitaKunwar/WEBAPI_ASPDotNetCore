using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HomeAPI.Models
{
    public class UserTotalExpense
    {
        [Key]
        public string UserID { get; set; }
        public decimal TotalPrice { get; set; }
        public string Username { get; set; }
    }
}
