using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Stage3FinalAppG23.Models;

namespace Stage3FinalAppG23.Pages.Using
{
    public class ClinicModel : PageModel
    {
        public List<string> ReceiptClinicNames = new List<string> { "North Clinic", "South Clinic", "East Clinic" };
        public List<string> ReceiptClinicAddress = new List<string> { "North Street", "South Street", "East Street" };
        public List<string> ReceiptClinicState = new List<string> { "California", "Florida", "Arkansas" };
        public List<string> ReceiptClinicCountry = new List<string> { "US", "US", "US" };
        public List<string> ReceiptClinicZipCode = new List<string> { "12345", "67890", "24680" };

        /// <RedirectcCheck>
        public string ClinicNameCheck { get; set; }
        public string UserAddressCheck { get; set; }
        public string PetNameCheck { get; set; }
        public string RebateName { get; set; }
        /// </summary>

        public string ReceiptFilePath { get; set; }

        [BindProperty]
        public Clinic Cln { get; set; }

        public Clinic CurrentCL { get; set; }

        public string Id { get; set; }

        public void OnGet(int? ID)
        {
            if (ID == 2)
            {
                HttpContext.Session.SetString("ReceiptFilePath", "Check");
            }

            Id = HttpContext.Session.GetString("ID");
            ClinicNameCheck = HttpContext.Session.GetString("ClinicName");

            ReceiptFilePath = HttpContext.Session.GetString("ReceiptFilePath");

            if (!string.IsNullOrEmpty(ReceiptFilePath) && string.IsNullOrEmpty(ClinicNameCheck))
            {
                int n = new Random().Next(2);
                int c = new Random().Next(2);
                int s = new Random().Next(2);
                int a = new Random().Next(2);
                int z = new Random().Next(2);

                CurrentCL = new Clinic();

                CurrentCL.Name = ReceiptClinicNames[n];
                CurrentCL.country = ReceiptClinicCountry[c];
                CurrentCL.state = ReceiptClinicState[s];
                CurrentCL.Address = ReceiptClinicAddress[a];
                CurrentCL.zipcode = ReceiptClinicZipCode[z];
                Cln = CurrentCL;
            }

            else
            {
                CurrentCL = new Clinic();

                CurrentCL.Name = HttpContext.Session.GetString("ClinicName");
                CurrentCL.country = HttpContext.Session.GetString("ClinicCountry");
                CurrentCL.state = HttpContext.Session.GetString("ClinicState");
                CurrentCL.Address = HttpContext.Session.GetString("ClinicAddress");
                CurrentCL.zipcode = HttpContext.Session.GetString("ClinicZipCode");

                Cln = CurrentCL;
            }
        }
        public IActionResult OnPost()
        {
            ReceiptFilePath = HttpContext.Session.GetString("ReceiptFilePath");

            ClinicNameCheck = HttpContext.Session.GetString("ClinicName");
            UserAddressCheck = HttpContext.Session.GetString("UserAddress");
            PetNameCheck = HttpContext.Session.GetString("PetName");
            RebateName = HttpContext.Session.GetString("RebateName");

            HttpContext.Session.SetString("ClinicName", Cln.Name);
            HttpContext.Session.SetString("ClinicCountry", Cln.country);
            HttpContext.Session.SetString("ClinicState", Cln.state);
            HttpContext.Session.SetString("ClinicAddress", Cln.Address);
            HttpContext.Session.SetString("ClinicZipCode", Cln.zipcode);

            if (!string.IsNullOrEmpty(ClinicNameCheck) && !string.IsNullOrEmpty(UserAddressCheck) && !string.IsNullOrEmpty(PetNameCheck) && !string.IsNullOrEmpty(RebateName))
            {
                return RedirectToPage("/Using/Summary");
            }
            else
            {
                return RedirectToPage("/Using/Details");
            }
        }
    }
}
