using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace WEBAPI_ASPDotNetCore.Models
{
    public class LoadImage
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]

        public int Id { get; set; }
        
        public string ImageName { get; set; }
     
        public string Description { get; set; }
    }
}
