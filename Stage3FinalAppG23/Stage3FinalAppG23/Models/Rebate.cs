using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace Stage3FinalAppG23.Models
{
    public class Rebate
    {
        [Required]
        [Display(Name = "Rebate Selection")]
        public string Name { get; set; }

        public string Description { get; set; }

        public void Rebate1()
        {
            Name = "Receive Card Via Mail";
            Description = "Receive A Physical Pre-Paid Mastercard Via Mail Delivery Within 6-8 Weeks After Valid Submission";
        }
        public void Rebate2()
        {
            Name = "Receive Card Via Email";
            Description = "Receive A Virtual Pre-Paid Mastercard" +
                "Via Email Delivery Within 2 Weeks After Valid Submission";
        }
    }
}
