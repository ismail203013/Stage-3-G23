using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Stage3FinalAppG23.Models;
using Stage3FinalAppG23.Pages.Database;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Stage3FinalAppG23.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }
        // Attributes for viewing all rebates, searching and filtering
        public List<Product> productsList { get; set; }

        public ConnectionString connection;

        [BindProperty]
        public string searchString { get; set; }
        public string Message;
        public string MessageInvalid;
        public bool Invalid = false;

        [BindProperty(SupportsGet = true)]
        public string Filter { get; set; }
        public List<string> FilterTypeList { get; set; } = new List<string> { "All", "Farm", "Pet" };

        public string ID { get; set; }

        public void OnGet()
        {
            // Session
            ID = HttpContext.Session.GetString("ID");

            Clear(ID);

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
                command.CommandText = @"SELECT * FROM Products";
                SqlDataReader reader = command.ExecuteReader();

                // Adding matching data to a list
                productsList = new List<Product>();
                while (reader.Read())
                {
                    Product prod = new Product();
                    prod.ID = reader.GetInt32(0);
                    prod.Name = reader.GetString(1);
                    prod.StartDate = reader.GetString(2);
                    prod.EndDate = reader.GetString(3);
                    prod.Description = reader.GetString(4);
                    prod.File = reader.GetString(5);
                    prod.FilterType = reader.GetString(6);
                    prod.LogoFile = reader.GetString(7);

                    productsList.Add(prod);
                }
                reader.Close();
            }
        }

        public IActionResult OnPostSearch()
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

                if (string.IsNullOrEmpty(searchString))
                {
                    command.CommandText = @"SELECT * FROM Products";
                    Invalid = true;
                }
                else
                {
                    command.CommandText = @"SELECT * FROM Products WHERE (ProductName LIKE '%' + @SearchTerm) OR (ProductName LIKE @SearchTerm + '%')";
                    command.Parameters.AddWithValue("@SearchTerm", searchString);
                }
                SqlDataReader reader = command.ExecuteReader();

                // Adding matching data to a list
                productsList = new List<Product>();
                while (reader.Read())
                {
                    Product prod = new Product();
                    prod.ID = reader.GetInt32(0);
                    prod.Name = reader.GetString(1);
                    prod.StartDate = reader.GetString(2);
                    prod.EndDate = reader.GetString(3);
                    prod.Description = reader.GetString(4);
                    prod.File = reader.GetString(5);
                    prod.FilterType = reader.GetString(6);
                    prod.LogoFile = reader.GetString(7);

                    productsList.Add(prod);
                }
                reader.Close();

                // Check if there is no results
                if (productsList.Count == 0)
                {
                    Message = "No results have been found";
                    return Page();
                }
                if (Invalid == true)
                {
                    MessageInvalid = "Please enter a valid term.";
                    Invalid = false;
                    return Page();
                }
            }
            return Page();
        }

        public IActionResult OnPostFilteringType()
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

                if (string.IsNullOrEmpty(Filter) || Filter == "All")
                {
                    command.CommandText = @"SELECT * FROM Products";
                }
                else
                {
                    command.CommandText += @"SELECT * FROM Products WHERE ProductFilterType = @Filtering";
                    command.Parameters.AddWithValue("@Filtering", Filter);
                }
                SqlDataReader reader = command.ExecuteReader();

                // Adding matching data to a list
                productsList = new List<Product>();
                while (reader.Read())
                {
                    Product prod = new Product();
                    prod.ID = reader.GetInt32(0);
                    prod.Name = reader.GetString(1);
                    prod.StartDate = reader.GetString(2);
                    prod.EndDate = reader.GetString(3);
                    prod.Description = reader.GetString(4);
                    prod.File = reader.GetString(5);
                    prod.FilterType = reader.GetString(6);
                    prod.LogoFile = reader.GetString(7);

                    productsList.Add(prod);
                }
                reader.Close();

                // Check if there is no results
                if (productsList.Count == 0)
                {
                    Message = "No results have been found";
                    return Page();
                }
            }
            return Page();
        }
        public void Clear(string S)
        {
            if (!string.IsNullOrEmpty(S))
            {
                HttpContext.Session.Remove("CurrentProductID");
                HttpContext.Session.Remove("CurrentProductName");

                HttpContext.Session.Remove("RebateName");
                HttpContext.Session.Remove("RebateDescription");
                HttpContext.Session.Remove("ClinicName");
                HttpContext.Session.Remove("ClinicCountry");
                HttpContext.Session.Remove("ClinicState");
                HttpContext.Session.Remove("ClinicAddress");
                HttpContext.Session.Remove("ClinicZipCode");
                HttpContext.Session.Remove("UserFirstName");
                HttpContext.Session.Remove("UserLastName");
                HttpContext.Session.Remove("UserEmail");
                HttpContext.Session.Remove("UserPhoneNumber");
                HttpContext.Session.Remove("UserAddress");
                HttpContext.Session.Remove("UserApartment");
                HttpContext.Session.Remove("UserZipCode");
                HttpContext.Session.Remove("UserCity");
                HttpContext.Session.Remove("UserCountry");
                HttpContext.Session.Remove("UserState");
                HttpContext.Session.Remove("PetName");
                HttpContext.Session.Remove("PetDOB");
                HttpContext.Session.Remove("ReceiptFilePath");
            }
            else
            {
                HttpContext.Session.Clear();
            }
        }
    }
}
