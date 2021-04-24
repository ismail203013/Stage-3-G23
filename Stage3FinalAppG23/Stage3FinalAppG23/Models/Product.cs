using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stage3FinalAppG23.Models
{
    public class Product
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string Description { get; set; }
        public string FilterType { get; set; }
        public string File { get; set; }
        public string LogoFile { get; set; }
    }
}
