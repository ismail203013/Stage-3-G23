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
    public class DetailsModel : PageModel
    {
            
        
            public List<string> DetailsFirstNames = new List<string> { "Mehmet", "Gloria", "Martin" };
            public List<string> DetailsLastNames = new List<string> { "Ozcan", "Hirsch", "Cooper" };
            public List<string> DetailsAddress = new List<string> { "123 South Street", "456 West Street", "789 North Street" };
            public List<string> DetailsZipCode = new List<string> { "53420", "86942", "90432" };
            public List<string> DetailsCity = new List<string> { "Miami", "Conway", "Honolulu" };
            public List<string> DetailsCountry = new List<string> { "US", "US", "US" };
            public List<string> DetailsState = new List<string> { "Florida", "Arkansas", "Hawaii" };

            /// <RedirectcCheck>
            public string ClinicNameCheck { get; set; }
            public string UserAddressCheck { get; set; }
            public string PetNameCheck { get; set; }
            public string RebateName { get; set; }
            /// </summary>

            public string ReceiptFilePath { get; set; }

            [BindProperty]
            public Details CurrentUserDetails { get; set; }

            public Details CurrentDT { get; set; }

            public string ID { get; set; }

            public string ErrorMessage { get; set; }

            public void OnGet()
            {
                ID = HttpContext.Session.GetString("ID");
                UserAddressCheck = HttpContext.Session.GetString("UserAddress");

                ReceiptFilePath = HttpContext.Session.GetString("ReceiptFilePath");

                if (!string.IsNullOrEmpty(ID) && string.IsNullOrEmpty(UserAddressCheck))
                {
                    ConnectionString dbstring = new ConnectionString();
                    string DbConnection = dbstring.DatabaseConn();
                    SqlConnection conn = new SqlConnection(DbConnection);
                    conn.Open();

                    CurrentDT = new Details();
                    using (SqlCommand command = new SqlCommand())
                    {
                        command.Connection = conn;

                        command.CommandText = @"SELECT * FROM UserDBO WHERE UserID = @ID";
                        command.Parameters.AddWithValue("ID", ID);

                        SqlDataReader reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            CurrentDT.FirstName = reader.GetString(1);
                            CurrentDT.LastName = reader.GetString(2);
                            CurrentDT.Email = reader.GetString(3);
                            if (!reader.IsDBNull(5))
                            { CurrentDT.phoneNumber = reader.GetString(5); }
                            if (!reader.IsDBNull(6))
                            { CurrentDT.Address = reader.GetString(6); }
                            if (!reader.IsDBNull(7))
                            { CurrentDT.Apartment = reader.GetString(7); }
                            if (!reader.IsDBNull(8))
                            { CurrentDT.zipcode = reader.GetString(8); }
                            if (!reader.IsDBNull(9))
                            { CurrentDT.City = reader.GetString(9); }
                            if (!reader.IsDBNull(10))
                            { CurrentDT.Country = reader.GetString(10); }
                            if (!reader.IsDBNull(11))
                            { CurrentDT.state = reader.GetString(11); }
                        }
                        reader.Close();
                    }

                    CurrentUserDetails = CurrentDT;
                }

                else if (!string.IsNullOrEmpty(ReceiptFilePath) && string.IsNullOrEmpty(UserAddressCheck))
                {
                    int Fn = new Random().Next(2);
                    int Ln = new Random().Next(2);
                    int A = new Random().Next(2);
                    int Z = new Random().Next(2);
                    int C = new Random().Next(2);
                    int SC = new Random().Next(2);

                    CurrentDT = new Details();

                    CurrentDT.FirstName = DetailsFirstNames[Fn];
                    CurrentDT.LastName = DetailsLastNames[Ln];
                    CurrentDT.Address = DetailsAddress[A];
                    CurrentDT.zipcode = DetailsZipCode[Z];
                    CurrentDT.City = DetailsCity[SC];
                    CurrentDT.Country = DetailsCountry[C];
                    CurrentDT.state = DetailsState[SC];
                    CurrentUserDetails = CurrentDT;
                }


                else
                {
                    CurrentDT = new Details();

                    CurrentDT.FirstName = HttpContext.Session.GetString("UserFirstName");
                    CurrentDT.LastName = HttpContext.Session.GetString("UserLastName");
                    CurrentDT.Email = HttpContext.Session.GetString("UserEmail");
                    CurrentDT.phoneNumber = HttpContext.Session.GetString("UserPhoneNumber");
                    CurrentDT.Address = HttpContext.Session.GetString("UserAddress");
                    CurrentDT.Apartment = HttpContext.Session.GetString("UserApartment");
                    CurrentDT.zipcode = HttpContext.Session.GetString("UserZipCode");
                    CurrentDT.City = HttpContext.Session.GetString("UserCity");
                    CurrentDT.Country = HttpContext.Session.GetString("UserCountry");
                    CurrentDT.state = HttpContext.Session.GetString("UserState");
                    CurrentUserDetails = CurrentDT;
                }

            }
            public IActionResult OnPostDetails()
            {
                ClinicNameCheck = HttpContext.Session.GetString("ClinicName");
                UserAddressCheck = HttpContext.Session.GetString("UserAddress");
                PetNameCheck = HttpContext.Session.GetString("PetName");
                RebateName = HttpContext.Session.GetString("RebateName");

                if (CurrentUserDetails.Email == CurrentUserDetails.redoEmail)
                {
                    HttpContext.Session.SetString("UserFirstName", CurrentUserDetails.FirstName);
                    HttpContext.Session.SetString("UserLastName", CurrentUserDetails.LastName);
                    HttpContext.Session.SetString("UserEmail", CurrentUserDetails.Email);
                    if (string.IsNullOrEmpty(CurrentUserDetails.phoneNumber))
                    {
                        CurrentUserDetails.phoneNumber = "Not Set";
                    }
                    HttpContext.Session.SetString("UserPhoneNumber", CurrentUserDetails.phoneNumber);
                    HttpContext.Session.SetString("UserAddress", CurrentUserDetails.Address);
                    if (string.IsNullOrEmpty(CurrentUserDetails.Apartment))
                    {
                        CurrentUserDetails.Apartment = "Not Set";
                    }
                    HttpContext.Session.SetString("UserApartment", CurrentUserDetails.Apartment);
                    HttpContext.Session.SetString("UserZipCode", CurrentUserDetails.zipcode);
                    HttpContext.Session.SetString("UserCity", CurrentUserDetails.City);
                    HttpContext.Session.SetString("UserCountry", CurrentUserDetails.Country);
                    HttpContext.Session.SetString("UserState", CurrentUserDetails.state);

                    if (!string.IsNullOrEmpty(ClinicNameCheck) && !string.IsNullOrEmpty(UserAddressCheck) && !string.IsNullOrEmpty(PetNameCheck) && !string.IsNullOrEmpty(RebateName))
                    {
                        return RedirectToPage("/Using/Summary");
                    }

                    else
                    {
                        return RedirectToPage("/Using/PetDetails");
                    }
                }

                else
                {
                    ErrorMessage = "Your Emails Do Not Match";
                    return Page();
                }
            }
        
           
            }
        }
   

