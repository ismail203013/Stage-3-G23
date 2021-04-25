using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Stage3FinalAppG23.Models;
using Stage3FinalAppG23.Pages.Database;


namespace Stage3FinalAppG23.Pages.Using
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        public User usr { get; set; }
        public string Message { get; set; }
        public string SessionID;

        public string Id { get; set; }
        public string ProductID { get; set; }

        public void OnGet(int? ID)
        {
            if (ID == 2)
            {
                HttpContext.Session.SetString("ReceiptFilePath", "Check");
            }

            ProductID = HttpContext.Session.GetString("CurrentProductID");
            Id = HttpContext.Session.GetString("ID");
        }

        public IActionResult OnPost()
        {
            ProductID = HttpContext.Session.GetString("CurrentProductID");
            Id = HttpContext.Session.GetString("ID");

            ConnectionString dbstring = new ConnectionString();
            string DbConnection = dbstring.DatabaseConn();
            SqlConnection conn = new SqlConnection(DbConnection);
            conn.Open();

            using (SqlCommand command = new SqlCommand())
            {
                command.Connection = conn;
                command.CommandText = @"SELECT UserID, UserFirstName, UserLastName, UserEmailAddress, UserPassword FROM UserDBO WHERE UserEmailAddress = @Email AND UserPassword = @Pwd";

                command.Parameters.AddWithValue("@Email", usr.EmailAddress);
                command.Parameters.AddWithValue("@Pwd", usr.Password);

                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    usr.ID = reader.GetInt32(0);
                    usr.FirstName = reader.GetString(1);
                    usr.EmailAddress = reader.GetString(2);
                    usr.Password = reader.GetString(3);
                }

                if (!string.IsNullOrEmpty(usr.FirstName))
                {
                    SessionID = HttpContext.Session.Id;
                    HttpContext.Session.SetString("sessionID", SessionID);

                    HttpContext.Session.SetString("ID", usr.ID.ToString());
                    HttpContext.Session.SetString("firstname", usr.FirstName);

                    if (string.IsNullOrEmpty(ProductID))
                    {
                        Console.WriteLine("INDEX");
                        return RedirectToPage("/Index");
                    }
                    else
                    {
                        Console.WriteLine("CLINIC");
                        return RedirectToPage("/Using/Clinic");
                    }
                }
                else
                {
                    Message = "Email And Password Do Not Match";
                    return Page();
                }
            }
        }
    }
}
