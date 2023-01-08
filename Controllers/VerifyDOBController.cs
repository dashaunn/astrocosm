using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Astrocosm.Models;
using Dapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;

namespace Astrocosm.Controllers
{
    public class VerifyDOBController : Controller
    {
        // Allow the controller to access and manage users/user information in the application
        private readonly UserManager<AppUser> _userManager;


        public VerifyDOBController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }


        // Display the birthdate verification page
        public IActionResult Index()
        {
            // Retrieve the user object
            var user = _userManager.GetUserAsync(HttpContext.User).Result;
            if (user == null)
            {
                // the user object is null
                Console.WriteLine("The user object is null");
            }
            else
            {
                // Show the user the birthdate they are verifying, minus the year (e.g. December 22) 
                ViewBag.TempDOB = user.DOB.ToString("MMMM dd");


                // ---- <Re-Format the DOB to fit database syntax>  ----

                // Extract the month and day from the verified DOB
                int month = user.DOB.Month;
                int day = user.DOB.Day;

                // Set year to match database year
                int year = 0001;

                // End of year overlap (Capricorn)
                if ((user.DOB.Month == 01) && (user.DOB.Day < 20))
                {
                    year = 0002;
                }

                // Create a new DateTime object with format that matches database syntax 'yyyy-MM-dd'
                DateTime fullDOB = new DateTime(year, month, day);
                // Update the user's DOB to match database syntax
                user.DOB = fullDOB;
                _userManager.UpdateAsync(user).Wait();

                // ---- </Re-Format the DOB to fit database syntax>  ----


                // Add MySql connection
                IConfiguration configuration = new ConfigurationBuilder()
                    .AddJsonFile("appsettings.json")
                    .Build();

                string connectionString = configuration.GetConnectionString("DefaultConnection");
                
                using (var connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    // Calculate the user's SunSignId
                    // @DOB is the user's verified DOB
                    string sql = "SELECT Id FROM ZodiacSigns WHERE StartDate <= @DOB AND @DOB <= EndDate";
                    int sunSignId = connection.QueryFirstOrDefault<int>(sql, new { DOB = user.DOB });

                    // Update the user's SunSignId
                    sql = "UPDATE AspNetUsers SET SunSignId = @SunSignId WHERE Id = @Id";
                    connection.Execute(sql, new { SunSignId = sunSignId, Id = user.Id });
                }
                
            }
                return View();

        }

    }
}
