using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Column(TypeName="nvarchar(50)")]
        public string Firstname { get; set; }
        [Column(TypeName = "nvarchar(50)")]
        public string Lastname { get; set; }
        [Column(TypeName = "nvarchar(50)")]
        public string Birthdate { get; set; }
        [Column(TypeName = "nvarchar(50)")]
        public string Gender { get; set; }
    }
}
