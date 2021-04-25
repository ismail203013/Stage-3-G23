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
    public class AccountModel : PageModel
    {
        [BindProperty]
        public User user { get; set; }
        public ConnectionString connection;
        public string Message;

        // Session
        public string ID { get; set; }

        public IActionResult OnGet()
        {
            ID = HttpContext.Session.GetString("ID");
            if (string.IsNullOrEmpty(ID))
            {
                return RedirectToPage("/Using/Login");
            }

            user = new User();
            user.ID = Convert.ToInt32(HttpContext.Session.GetString("ID"));

            // Get connection string from DB
            connection = new ConnectionString();
            string DbConnection = connection.DatabaseConn();

            // Get SQL connection
            SqlConnection conn = new SqlConnection(DbConnection);
            conn.Open();

            // Get data from DB
            using (SqlCommand command = new SqlCommand())
            {
                command.Connection = conn;
                command.CommandText = @"SELECT * FROM UserDBO WHERE UserID = @UID";
                command.Parameters.AddWithValue("@UID", user.ID);

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    user.ID = reader.GetInt32(0);
                    user.FirstName = reader.GetString(1);
                    user.LastName = reader.GetString(2);
                    user.EmailAddress = reader.GetString(3);
                    user.Password = reader.GetString(4);

                    if (!reader.IsDBNull(5))
                    {
                        user.PhoneNumber = reader.GetString(5);
                    }
                    if (!reader.IsDBNull(6))
                    {
                        user.StreetAddress = reader.GetString(6);
                    }
                    if (!reader.IsDBNull(7))
                    {
                        user.Apartment = reader.GetString(7);
                    }
                    if (!reader.IsDBNull(8))
                    {
                        user.ZipCode = reader.GetString(8);
                    }
                    if (!reader.IsDBNull(9))
                    {
                        user.City = reader.GetString(9);
                    }
                    if (!reader.IsDBNull(10))
                    {
                        user.Country = reader.GetString(10);
                    }
                    if (!reader.IsDBNull(11))
                    {
                        user.State = reader.GetString(11);
                    }
                    if (!reader.IsDBNull(12))
                    {
                        user.Pet1Name = reader.GetString(12);
                    }
                    if (!reader.IsDBNull(13))
                    {
                        user.Pet1Birthday = reader.GetString(13);
                    }
                    if (!reader.IsDBNull(14))
                    {
                        user.Pet2Name = reader.GetString(14);
                    }
                    if (!reader.IsDBNull(15))
                    {
                        user.Pet2Birthday = reader.GetString(15);
                    }
                    if (!reader.IsDBNull(16))
                    {
                        user.Pet3Name = reader.GetString(16);
                    }
                    if (!reader.IsDBNull(17))
                    {
                        user.Pet3Birthday = reader.GetString(17);
                    }
                }
            }
            return Page();
        }
        public IActionResult OnPost()
        {
            // Session
            ID = HttpContext.Session.GetString("ID");

            // Get connection string from DB
            connection = new ConnectionString();
            string DbConnection = connection.DatabaseConn();

            // Get SQL connection
            SqlConnection conn = new SqlConnection(DbConnection);
            conn.Open();

            // Get data from DB
            using (SqlCommand command = new SqlCommand())
            {
                command.Connection = conn;
                command.CommandText = @"UPDATE UserDBO SET UserFirstName = @UFN, UserLastName = @ULN, UserEmailAddress = @UEA, 
                UserPhoneNumber = @UPN, UserStreetAddress = @USA, UserApartment = @UA, UserZipCode = @UZC, UserCity = @UCity,
                UserCountry = @UCountry, UserState = @UState, UserPet1Name = @UP1N, 
                UserPet1Birthday = @UP1B, UserPet2Name = @UP2N, UserPet2Birthday = @UP2B, UserPet3Name = @UP3N,
                UserPet3Birthday = @UP3B WHERE UserID = @UID";

                command.Parameters.AddWithValue("@UID", user.ID);
                command.Parameters.AddWithValue("@UFN", user.FirstName);
                command.Parameters.AddWithValue("@ULN", user.LastName);
                command.Parameters.AddWithValue("@UEA", user.EmailAddress);

                // User
                if (string.IsNullOrEmpty(user.PhoneNumber))
                {
                    command.Parameters.AddWithValue("@UPN", DBNull.Value);
                }
                else
                {
                    command.Parameters.AddWithValue("@UPN", user.PhoneNumber);
                }

                if (string.IsNullOrEmpty(user.StreetAddress))
                {
                    command.Parameters.AddWithValue("@USA", DBNull.Value);
                }
                else
                {
                    command.Parameters.AddWithValue("@USA", user.StreetAddress);
                }

                if (string.IsNullOrEmpty(user.Apartment))
                {
                    command.Parameters.AddWithValue("@UA", DBNull.Value);
                }
                else
                {
                    command.Parameters.AddWithValue("@UA", user.Apartment);
                }
                if (string.IsNullOrEmpty(user.ZipCode))
                {
                    command.Parameters.AddWithValue("@UZC", DBNull.Value);
                }
                else
                {
                    command.Parameters.AddWithValue("@UZC", user.ZipCode);
                }
                if (string.IsNullOrEmpty(user.City))
                {
                    command.Parameters.AddWithValue("@UCity", DBNull.Value);
                }
                else
                {
                    command.Parameters.AddWithValue("@UCity", user.City);
                }
                if (string.IsNullOrEmpty(user.Country))
                {
                    command.Parameters.AddWithValue("@UCountry", DBNull.Value);
                }
                else
                {
                    command.Parameters.AddWithValue("@UCountry", user.Country);
                }
                if (string.IsNullOrEmpty(user.State))
                {
                    command.Parameters.AddWithValue("@UState", DBNull.Value);
                }
                else
                {
                    command.Parameters.AddWithValue("@UState", user.State);
                }

                // Pets
                if (string.IsNullOrEmpty(user.Pet1Name))
                {
                    command.Parameters.AddWithValue("@UP1N", DBNull.Value);
                }
                else
                {
                    command.Parameters.AddWithValue("@UP1N", user.Pet1Name);
                }
                if (string.IsNullOrEmpty(user.Pet1Birthday))
                {
                    command.Parameters.AddWithValue("@UP1B", DBNull.Value);
                }
                else
                {
                    command.Parameters.AddWithValue("@UP1B", user.Pet1Birthday);
                }
                if (string.IsNullOrEmpty(user.Pet2Name))
                {
                    command.Parameters.AddWithValue("@UP2N", DBNull.Value);
                }
                else
                {
                    command.Parameters.AddWithValue("@UP2N", user.Pet2Name);
                }
                if (string.IsNullOrEmpty(user.Pet2Birthday))
                {
                    command.Parameters.AddWithValue("@UP2B", DBNull.Value);
                }
                else
                {
                    command.Parameters.AddWithValue("@UP2B", user.Pet2Birthday);
                }
                if (string.IsNullOrEmpty(user.Pet3Name))
                {
                    command.Parameters.AddWithValue("@UP3N", DBNull.Value);
                }
                else
                {
                    command.Parameters.AddWithValue("@UP3N", user.Pet3Name);
                }
                if (string.IsNullOrEmpty(user.Pet3Birthday))
                {
                    command.Parameters.AddWithValue("@UP3B", DBNull.Value);
                }
                else
                {
                    command.Parameters.AddWithValue("@UP3B", user.Pet3Birthday);
                }

                command.ExecuteNonQuery();
            }
            conn.Close();
            Message = "Account details updated successfully!";
            return Page();
        }
    }
}
