using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Astrocosm.Data;
using Astrocosm.Models;
using Dapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Astrocosm.Controllers
{
    public class CastChartController : Controller
    {
        // Allow the controller to access and manage users/user information in the application
        private readonly UserManager<AppUser> _userManager;


        public CastChartController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }


        // GET: /<controller>/
        public IActionResult Index()
        {
            // Retrieve the user object
            AppUser user = _userManager.GetUserAsync(HttpContext.User).Result;
            if (user == null)
            {
                // the user object is null
                Console.WriteLine("The user object is null");
            }
            else
            {
                _userManager.UpdateAsync(user).Wait();

                // Add MySql connection
                IConfiguration configuration = new ConfigurationBuilder()
                    .AddJsonFile("appsettings.json")
                    .Build();

                string connectionString = configuration.GetConnectionString("DefaultConnection");

                using (var connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    // Fetch the SunInfo (Introductory info about the sun in astrology)
                    string sql = "SELECT * FROM SunInfo";
                    SunInfo sunData = connection.QueryFirst<SunInfo>(sql);

                    // Store the SunData in a viewbag
                    ViewBag.SunInfo = sunData;

                    // Fetch the related ZodiacSign entity using the updated SunSignId
                    sql = "SELECT * FROM ZodiacSigns WHERE Id = @Id";
                    ZodiacSign sunSign = connection.QueryFirst<ZodiacSign>(sql, new { Id = user.SunSignId });

                    // Store the ZodiacSign entity in a viewbag
                    ViewBag.SunSign = sunSign;
                }
            }

            return View();

        }

        public IActionResult MyChart()
        {
            // Retrieve the user object
            AppUser user = _userManager.GetUserAsync(HttpContext.User).Result;
            if (user == null)
            {
                // the user object is null
                Console.WriteLine("The user object is null");
            }
            else
            {
                _userManager.UpdateAsync(user).Wait();

                // Add MySql connection
                IConfiguration configuration = new ConfigurationBuilder()
                    .AddJsonFile("appsettings.json")
                    .Build();

                string connectionString = configuration.GetConnectionString("DefaultConnection");

                using (var connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    // Fetch the SunInfo (Introductory info about the sun in astrology)
                    string sql = "SELECT * FROM SunInfo";
                    SunInfo sunData = connection.QueryFirst<SunInfo>(sql);

                    // Store the SunData in a viewbag
                    ViewBag.SunInfo = sunData;

                    // Fetch the related ZodiacSign entity using the updated SunSignId
                    sql = "SELECT * FROM ZodiacSigns WHERE Id = @Id";
                    ZodiacSign sunSign = connection.QueryFirst<ZodiacSign>(sql, new { Id = user.SunSignId });

                    // Store the ZodiacSign entity in a viewbag
                    ViewBag.SunSign = sunSign;
                }
            }

            return View("MyChart");
        }
            
    }
}


