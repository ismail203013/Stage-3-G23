using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Stage3FinalAppG23.Models;
using Stage3FinalAppG23.Pages.Database;

namespace Stage3FinalAppG23.Pages.Using
{
    public class UploadReceiptModel : PageModel
    {
        public Product CurrentProduct { get; set; }
        public ConnectionString connection;

        public int ProductID { get; set; }

        public string Id { get; set; }

        public int SimulateReceipt { get; set; }

        // attributes for receipt uploader
        [BindProperty(SupportsGet = true)]
        public IFormFile ReceiptFile { get; set; }
        public readonly IWebHostEnvironment _env;
        public UploadReceiptModel(IWebHostEnvironment env)
        {
            _env = env;
        }

        public IActionResult OnGet(int? ID)
        {
            HttpContext.Session.Remove("ReceiptFilePath");

            SimulateReceipt = 2;

            Id = HttpContext.Session.GetString("ID");
            ProductID = Convert.ToInt32(HttpContext.Session.GetString("CurrentProductID"));

            connection = new ConnectionString();
            string DbConnection = connection.DatabaseConn();

            SqlConnection conn = new SqlConnection(DbConnection);
            conn.Open();

            using (SqlCommand command = new SqlCommand())
            {
                command.Connection = conn;
                command.CommandText = @"SELECT * FROM Products WHERE ProductID = @ID";
                command.Parameters.AddWithValue("@ID", ID);

                SqlDataReader reader = command.ExecuteReader();
                CurrentProduct = new Product();

                while (reader.Read())
                {
                    CurrentProduct.ID = reader.GetInt32(0);
                    CurrentProduct.Name = reader.GetString(1);
                    CurrentProduct.StartDate = reader.GetString(2);
                    CurrentProduct.EndDate = reader.GetString(3);
                    CurrentProduct.Description = reader.GetString(4);
                    CurrentProduct.File = reader.GetString(6);
                }
            }
            HttpContext.Session.SetString("CurrentProductName", CurrentProduct.Name);
            HttpContext.Session.SetString("CurrentProductID", CurrentProduct.ID.ToString());
            return Page();
        }
    }
}
