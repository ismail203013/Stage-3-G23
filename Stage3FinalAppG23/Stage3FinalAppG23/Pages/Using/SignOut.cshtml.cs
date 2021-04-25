using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Stage3FinalAppG23.Pages.Using
{
    public class SignOutModel : PageModel
    {
        public string Message;

        public string ID { get; set; }

        public IActionResult OnGet()
        {
            ID = HttpContext.Session.GetString("ID");

            HttpContext.Session.Clear();
            Message = "You have been successfully signed out.";

            return Page();
        }
    }
}
