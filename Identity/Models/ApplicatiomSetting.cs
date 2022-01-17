using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.Models
{
    public class ApplicatiomSetting
    {
        public string validIssuer { get; set; }
        public string validAudience { get; set; }
        public string Secret { get; set; }
        public string expiryInMinutes { get; set; }
    }
}
