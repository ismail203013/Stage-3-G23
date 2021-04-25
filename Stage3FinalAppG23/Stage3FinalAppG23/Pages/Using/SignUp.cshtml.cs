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
    public class SignUpModel : PageModel
    {
        [BindProperty]
        public User currentUser { get; set; }
        public ConnectionString connection;

        public User CheckExists { get; set; }

        public string ID { get; set; }

        public string ErrorMessage { get; set; }

        public void OnGet()
        {
            ID = HttpContext.Session.GetString("ID");
        }


        public IActionResult OnPost()
        {
            ID = HttpContext.Session.GetString("ID");

            if (currentUser.Password != currentUser.redoPassword)
            {
                ErrorMessage = "Your Passwords Do Not Match";
                return Page();
            }
            else if (currentUser.Password.Length < 5)
            {
                ErrorMessage = "Your Password Must Be Over 5 Characters Long";
                return Page();
            }

            connection = new ConnectionString();
            string DbConnection = connection.DatabaseConn();

            SqlConnection conn = new SqlConnection(DbConnection);
            conn.Open();

            using (SqlCommand command1 = new SqlCommand())
            {
                command1.Connection = conn;
                command1.CommandText = @"SELECT UserEmailAddress from UserDBO WHERE UserEmailAddress = @Email";
                command1.Parameters.AddWithValue("@Email", currentUser.EmailAddress);

                SqlDataReader reader = command1.ExecuteReader();
                CheckExists = new User();

                while (reader.Read())
                {
                    CheckExists.EmailAddress = reader.GetString(0);
                }
                reader.Close();
            }
            if (currentUser.EmailAddress == CheckExists.EmailAddress)
            {
                ErrorMessage = "Email Has Already Been Taken.";
                return Page();
            }
            else
            {
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = conn;
                    command.CommandText = @"INSERT INTO UserDBO (UserFirstName, UserLastName, UserEmailAddress, UserPassword) VALUES (@FName, @LName, @Email, @Pwd)";

                    command.Parameters.AddWithValue("@FName", currentUser.FirstName);
                    command.Parameters.AddWithValue("@LName", currentUser.LastName);
                    command.Parameters.AddWithValue("@Email", currentUser.EmailAddress);
                    command.Parameters.AddWithValue("@Pwd", currentUser.Password);

                    command.ExecuteNonQuery();
                }

                return RedirectToPage("/Using/LogIn");
            }
        }
    }
}
