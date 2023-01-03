using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Astrocosm.Data;
using Astrocosm.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Astrocosm.Controllers
{
    public class CalculateController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            //Display the calculator
            return View();
        }


        public CalculateController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }


        private readonly UserManager<AppUser> _userManager;

        [HttpPost("/calculate")]
        public IActionResult SunSignCalculation(DateTime DOB)
        {
            // retrieve the Asp net identity user object
            var user = _userManager.GetUserAsync(HttpContext.User).Result;
            if (user == null)
            {
                // the user object is null, add some debugging information or throw an exception
                Console.WriteLine("The user object is null");
            }
            // set the DOB property of the user object
            user.DOB = DOB;

            // save the updated user object
            _userManager.UpdateAsync(user).Wait();

            // Connect to the MySQL database and retrieve the CSV file
            string connectionString = "Server=localhost;Database=astrocosm;Uid=astrocosm;Pwd=Im@g1n8ti0n;";
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                // Retrieve the CSV file from the database
                string query = "SELECT * FROM ZodiacSigns";
                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();

                // Parse the CSV file and store the date ranges in a dictionary
                Dictionary<string, DateTime[]> sunSignDates = new Dictionary<string, DateTime[]>();
                string sunSign = "";
                string startDateString = "";
                string endDateString = "";
                int sunSignId;
                while (reader.Read())
                {
                    sunSign = reader["LatinName"].ToString();
                    startDateString = reader["StartDate"].ToString();
                    endDateString = reader["EndDate"].ToString();
                    sunSignId = (int)reader["Id"];

                    // Parse the start and end dates
                    DateTime startDate = DateTime.ParseExact(startDateString, "MM-dd", CultureInfo.InvariantCulture);
                    DateTime endDate = DateTime.ParseExact(endDateString, "MM-dd", CultureInfo.InvariantCulture);

                    // Store the date range in the dictionary
                    sunSignDates[sunSign] = new DateTime[] { startDate, endDate };
                }

                reader.Close();
                connection.Close();

                // Get the user's birth date from the calculation page
                string birthDateString = Request.Form["DOB"];

                // Extract the month and day from the birth date
                DateTime birthDate = DateTime.ParseExact(birthDateString, "yyyy-MM-dd", CultureInfo.InvariantCulture);
                int birthMonth = birthDate.Month;
                int birthDay = birthDate.Day;

                // Loop through the dictionary of sun sign date ranges and determine the user's sun sign
                foreach (KeyValuePair<string, DateTime[]> dateRange in sunSignDates)
                {
                    if (birthDate >= dateRange.Value[0] && birthDate <= dateRange.Value[1])
                    {
                        sunSign = dateRange.Key;
                        break;
                    }
                }

                // Display the user's sun sign information
                ViewData["SunSign"] = sunSign;
                return View();
            }

        }
    }
}