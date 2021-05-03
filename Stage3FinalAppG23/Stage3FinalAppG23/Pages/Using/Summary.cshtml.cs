using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Stage3FinalAppG23.Models;
using Stage3FinalAppG23.Pages.Database;

namespace Stage3FinalAppG23.Pages.Using
{
    public class SummaryModel : PageModel
    {
        public Product product { get; set; }

        public Rebate rebate { get; set; }

        public Clinic CurrentCL { get; set; }

        public Details CurrentDT { get; set; }

        public Pet CurrentPet { get; set; }

        public string ID { get; set; }

        public string ReceiptFilePath { get; set; }

        public string Message { get; set; }

        public void OnGet()
        {
            ID = HttpContext.Session.GetString("ID");
            ReceiptFilePath = HttpContext.Session.GetString("ReceiptFilePath");

            GetProduct();
            GetRebate();
            GetClinic();
            GetDetails();
            GetPet();
        }

        public void GetProduct()
        {
            product = new Product();

            product.ID = Convert.ToInt32(HttpContext.Session.GetString("CurrentProductID"));
            product.Name = HttpContext.Session.GetString("CurrentProductName");

            ConnectionString dbstring = new ConnectionString();
            string DbConnection = dbstring.DatabaseConn();
            SqlConnection conn = new SqlConnection(DbConnection);
            conn.Open();

            using (SqlCommand command = new SqlCommand())
            {
                command.Connection = conn;

                command.CommandText = @"SELECT * FROM Products WHERE ProductID = @ID AND ProductName = @Name";
                command.Parameters.AddWithValue("@ID", product.ID);
                command.Parameters.AddWithValue("@Name", product.Name);

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    product.ID = reader.GetInt32(0);
                    product.Name = reader.GetString(1);
                    product.StartDate = reader.GetString(2);
                    product.EndDate = reader.GetString(3);
                    product.Description = reader.GetString(4);
                    product.File = reader.GetString(6);
                }
                reader.Close();
            }
        }
        public void GetRebate()
        {
            rebate = new Rebate
            {
                Name = HttpContext.Session.GetString("RebateName"),
                Description = HttpContext.Session.GetString("RebateDescription")
            };

        }
        public void GetClinic()
        {
            CurrentCL = new Clinic
            {
                Name = HttpContext.Session.GetString("ClinicName"),
                country = HttpContext.Session.GetString("ClinicCountry"),
                state = HttpContext.Session.GetString("ClinicState"),
                Address = HttpContext.Session.GetString("ClinicAddress"),
                zipcode = HttpContext.Session.GetString("ClinicZipCode")
            };
        }
        public void GetDetails()
        {
            CurrentDT = new Details
            {
                FirstName = HttpContext.Session.GetString("UserFirstName"),
                LastName = HttpContext.Session.GetString("UserLastName"),
                Email = HttpContext.Session.GetString("UserEmail"),
                phoneNumber = HttpContext.Session.GetString("UserPhoneNumber"),
                Address = HttpContext.Session.GetString("UserAddress"),
                Apartment = HttpContext.Session.GetString("UserApartment"),
                zipcode = HttpContext.Session.GetString("UserZipCode"),
                City = HttpContext.Session.GetString("UserCity"),
                Country = HttpContext.Session.GetString("UserCountry"),
                state = HttpContext.Session.GetString("UserState")
            };
        }
        public void GetPet()
        {
            CurrentPet = new Pet
            {
                Name = HttpContext.Session.GetString("PetName"),
                DOB = HttpContext.Session.GetString("PetDOB")
            };
            if (CurrentPet.DOB != "Not Set")
            {
                DateTime Date = DateTime.Parse(CurrentPet.DOB);
                CurrentPet.DOB = Date.ToString("MM/dd/yyyy");
            }
            //Referenced in portfolio
        }

        public IActionResult OnPost()
        {
            return RedirectToPage("/Index");
        }
    }
}
