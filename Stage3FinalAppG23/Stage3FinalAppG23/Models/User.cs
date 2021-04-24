using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stage3FinalAppG23.Models
{
    public class User
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string Password { get; set; }
        public string redoPassword { get; set; }
        public bool UserOffers { get; set; }
        public string PhoneNumber { get; set; }
        public string StreetAddress { get; set; }
        public string Apartment { get; set; }
        public string ZipCode { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string State { get; set; }

        public string Pet1Name { get; set; }
        public string Pet1Birthday { get; set; }
        public string Pet2Name { get; set; }
        public string Pet2Birthday { get; set; }
        public string Pet3Name { get; set; }
        public string Pet3Birthday { get; set; }
    }
}
