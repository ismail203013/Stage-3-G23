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
    public class RebateChoiceModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public string Rebate { get; set; }

        public string Message { get; set; }

        public Rebate One = new Rebate();
        public Rebate Two = new Rebate();
        public List<Rebate> Choices = new List<Rebate>();

        public string ID { get; set; }

        public void OnGet()
        {
            ID = HttpContext.Session.GetString("ID");

            One.Rebate1();
            Two.Rebate2();
            Choices.Add(One);
            Choices.Add(Two);
        }

        public IActionResult OnPost()
        {
            ID = HttpContext.Session.GetString("ID");

            One.Rebate1();
            Two.Rebate2();
            Choices.Add(One);
            Choices.Add(Two);

            if (string.IsNullOrEmpty(Rebate))
            {
                Message = "Please Select A Rebate";
                return Page();
            }
            if (Rebate == Choices[0].Name)
            {
                HttpContext.Session.SetString("RebateName", One.Name);
                HttpContext.Session.SetString("RebateDescription", One.Description);
            }
            else if (Rebate == Choices[1].Name)
            {
                HttpContext.Session.SetString("RebateName", Two.Name);
                HttpContext.Session.SetString("RebateDescription", Two.Description);
            }
            return RedirectToPage("/Using/Summary");
        }
    }
}
