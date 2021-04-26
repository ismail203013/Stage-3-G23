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
    public class PetDetailsModel : PageModel
    {
        [BindProperty]
        public Pet CurrentPet { get; set; }
        public Pet Temp { get; set; }

        public Pet New { get; set; }
        public string ID { get; set; }

        public List<string> RandomPetNames = new List<string> { "Boxer", "Sparkles", "Jerry" };
        public List<string> RandomPetDOB = new List<string> { "2018-02-19", "2015-06-14", "2017-08-02" };

        /// <RedirectcCheck>
        public string ClinicNameCheck { get; set; }
        public string UserAddressCheck { get; set; }
        public string PetNameCheck { get; set; }
        public string RebateName { get; set; }
        /// </summary>

        public string ReceiptFilePath { get; set; }

        public List<Pet> PetList { get; set; } = new List<Pet>();
        public Pet Pet1 { get; set; }
        public Pet Pet2 { get; set; }
        public Pet Pet3 { get; set; }

        [BindProperty(SupportsGet = true)]
        public string RadioString { get; set; }

        public string Message { get; set; }

        public void OnGet()
        {
            ID = HttpContext.Session.GetString("ID");
            PetNameCheck = HttpContext.Session.GetString("PetName");
            ReceiptFilePath = HttpContext.Session.GetString("ReceiptFilePath");
            if (!string.IsNullOrEmpty(ReceiptFilePath) && string.IsNullOrEmpty(PetNameCheck))
            {
                int N = new Random().Next(2);
                int D = new Random().Next(2);
                Temp = new Pet();
                Temp.Name = RandomPetNames[N];
                Temp.DOB = RandomPetDOB[D];

                CurrentPet = Temp;
            }
            else
            {
                Temp = new Pet();
                Temp.Name = HttpContext.Session.GetString("PetName");
                Temp.DOB = HttpContext.Session.GetString("PetDOB");
                CurrentPet = Temp;
            }
            if (!string.IsNullOrEmpty(ID))
            {
                ConnectionString dbstring = new ConnectionString();
                string DbConnection = dbstring.DatabaseConn();
                SqlConnection conn = new SqlConnection(DbConnection);
                conn.Open();

                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = conn;

                    command.CommandText = @"SELECT UserPet1Name, UserPet1Birthday, UserPet2Name, UserPet2Birthday, UserPet3Name, UserPet3Birthday FROM UserDBO WHERE UserID = @ID";
                    command.Parameters.AddWithValue("ID", ID);

                    SqlDataReader reader = command.ExecuteReader();

                    Pet1 = new Pet();
                    Pet2 = new Pet();
                    Pet3 = new Pet();
                    while (reader.Read())
                    {
                        if (!reader.IsDBNull(0))
                        { Pet1.Name = reader.GetString(0); }
                        if (!reader.IsDBNull(1))
                        { Pet1.DOB = reader.GetString(1); }
                        if (!reader.IsDBNull(2))
                        { Pet2.Name = reader.GetString(2); }
                        if (!reader.IsDBNull(3))
                        { Pet2.DOB = reader.GetString(3); }
                        if (!reader.IsDBNull(4))
                        { Pet3.Name = reader.GetString(4); }
                        if (!reader.IsDBNull(5))
                        { Pet3.DOB = reader.GetString(5); }

                    }
                    reader.Close();
                    if (!string.IsNullOrEmpty(Pet1.Name))
                    {
                        PetList.Add(Pet1);
                    }
                    if (!string.IsNullOrEmpty(Pet2.Name))
                    {
                        PetList.Add(Pet2);
                    }
                    if (!string.IsNullOrEmpty(Pet3.Name))
                    {
                        PetList.Add(Pet3);
                    }
                }
            }
        }
        public IActionResult OnPost()
        {

            ID = HttpContext.Session.GetString("ID");

            ClinicNameCheck = HttpContext.Session.GetString("ClinicName");
            UserAddressCheck = HttpContext.Session.GetString("UserAddress");
            PetNameCheck = HttpContext.Session.GetString("PetName");
            RebateName = HttpContext.Session.GetString("RebateName");

            if (!string.IsNullOrEmpty(ID))
            {
                ConnectionString dbstring = new ConnectionString();
                string DbConnection = dbstring.DatabaseConn();
                SqlConnection conn = new SqlConnection(DbConnection);
                conn.Open();

                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = conn;

                    command.CommandText = @"SELECT UserPet1Name, UserPet1Birthday, UserPet2Name, UserPet2Birthday, UserPet3Name, UserPet3Birthday FROM UserDBO WHERE UserID = @ID";
                    command.Parameters.AddWithValue("ID", ID);

                    SqlDataReader reader = command.ExecuteReader();

                    Pet1 = new Pet();
                    Pet2 = new Pet();
                    Pet3 = new Pet();
                    while (reader.Read())
                    {
                        if (!reader.IsDBNull(0))
                        { Pet1.Name = reader.GetString(0); }
                        if (!reader.IsDBNull(1))
                        { Pet1.DOB = reader.GetString(1); }
                        if (!reader.IsDBNull(2))
                        { Pet2.Name = reader.GetString(2); }
                        if (!reader.IsDBNull(3))
                        { Pet2.DOB = reader.GetString(3); }
                        if (!reader.IsDBNull(4))
                        { Pet3.Name = reader.GetString(4); }
                        if (!reader.IsDBNull(5))
                        { Pet3.DOB = reader.GetString(5); }

                    }
                    reader.Close();
                    if (!string.IsNullOrEmpty(Pet1.Name))
                    {
                        PetList.Add(Pet1);
                    }
                    if (!string.IsNullOrEmpty(Pet2.Name))
                    {
                        PetList.Add(Pet2);
                    }
                    if (!string.IsNullOrEmpty(Pet3.Name))
                    {
                        PetList.Add(Pet3);
                    }
                }
            }

            if (string.IsNullOrEmpty(RadioString) &&
               string.IsNullOrEmpty(CurrentPet.Name) &&
               !string.IsNullOrEmpty(ID) &&
               PetList.Count > 0)
            {
                Message = "Please Select One Of Your Pets Or Enter A Pet Name Into The Pet Field";
                return Page();
            }

            else if (string.IsNullOrEmpty(CurrentPet.Name) &&
               !string.IsNullOrEmpty(ID) &&
               PetList.Count == 0)
            {
                Message = "Please Enter A Pet Name Into The Pet Field Or Add A Pet Into Your Account To Select It";
                return Page();
            }

            else if (!string.IsNullOrEmpty(RadioString))
            {
                if (RadioString == PetList[0].Name)
                {
                    HttpContext.Session.SetString("PetName", PetList[0].Name);
                    if (!string.IsNullOrEmpty(PetList[0].DOB))
                    {
                        HttpContext.Session.SetString("PetDOB", PetList[0].DOB);
                    }
                    else
                    {
                        HttpContext.Session.SetString("PetDOB", "Not Set");
                    }
                }
                if (PetList.Count > 1)
                {
                    if (RadioString == PetList[1].Name)
                    {
                        HttpContext.Session.SetString("PetName", PetList[1].Name);
                        if (!string.IsNullOrEmpty(PetList[1].DOB))
                        {
                            HttpContext.Session.SetString("PetDOB", PetList[1].DOB);
                        }
                        else
                        {
                            HttpContext.Session.SetString("PetDOB", "Not Set");
                        }
                    }
                }
                if (PetList.Count > 2)
                {
                    if (RadioString == PetList[2].Name)
                    {
                        HttpContext.Session.SetString("PetName", PetList[2].Name);
                        if (!string.IsNullOrEmpty(PetList[2].DOB))
                        {
                            HttpContext.Session.SetString("PetDOB", PetList[2].DOB);
                        }
                        else
                        {
                            HttpContext.Session.SetString("PetDOB", "Not Set");
                        }
                    }
                }
                if (!string.IsNullOrEmpty(ClinicNameCheck) && !string.IsNullOrEmpty(UserAddressCheck) && !string.IsNullOrEmpty(PetNameCheck) && !string.IsNullOrEmpty(RebateName))
                {
                    return RedirectToPage("/Using/Summary");
                }
                else
                {
                    return RedirectToPage("/Using/RebateChoice");
                }
            }

            else if (string.IsNullOrEmpty(RadioString) && !string.IsNullOrEmpty(CurrentPet.Name))
            {
                if (CurrentPet.Name.Length < 3)
                {
                    Message = "Pet Name Not Long Enough";
                    return Page();
                }

                HttpContext.Session.SetString("PetName", CurrentPet.Name);
                if (string.IsNullOrEmpty(CurrentPet.DOB))
                {
                    CurrentPet.DOB = "Not Set";
                }
                HttpContext.Session.SetString("PetDOB", CurrentPet.DOB);

                if (!string.IsNullOrEmpty(ClinicNameCheck) && !string.IsNullOrEmpty(UserAddressCheck) && !string.IsNullOrEmpty(PetNameCheck) && !string.IsNullOrEmpty(RebateName))
                {
                    return RedirectToPage("/Using/Summary");
                }
                else
                {
                    return RedirectToPage("/Using/RebateChoice");
                }
            }
            else
            {
                Message = "Please Enter A Valid Pet Name";
                return Page();
            }
        }
    }
}
