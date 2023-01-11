using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using Astrocosm.Models;
using Dapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;


namespace Astrocosm.Controllers
{
    public class HoroscopeController : Controller
    {
        // Allow the controller to access and manage users/user information in the application
        private readonly UserManager<AppUser> _userManager;

        public HoroscopeController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        // Display the Horoscope page
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

                    // Fetch the related ZodiacSign entity using the updated SunSignId
                    string sql = "SELECT * FROM ZodiacSigns WHERE Id = @Id";
                    ZodiacSign sunSign = connection.QueryFirst<ZodiacSign>(sql, new { Id = user.SunSignId });

                    // Store the ZodiacSign entity in a viewbag
                    ViewBag.SunSign = sunSign;
                }

            }
                return View();
        }


        // Take in the user's choice of day and assign the user's appropriate sign
        public async Task<IActionResult> GetHoroscope(string day, string sign)
        {
            AppUser user = await _userManager.GetUserAsync(HttpContext.User);
            _userManager.UpdateAsync(user).Wait();
            if (user == null)
            {   // the user object is null
                Console.WriteLine("The user object is null");
            }

            // Create the HttpClient and HttpRequestMessage objects
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri($"https://sameer-kumar-aztro-v1.p.rapidapi.com/?sign={sign}&day={day}"),
                Headers =
        {
            { "X-RapidAPI-Key", "0deb55af4dmsh44dec64dc2bcc8cp1fd813jsnb1ecc5d89de0" },
            { "X-RapidAPI-Host", "sameer-kumar-aztro-v1.p.rapidapi.com" },
        },
            };

            // Send the HTTP POST request and retrieve the response
            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var json = await response.Content.ReadAsStringAsync();

                // Deserialize the JSON object into an instance of the Horoscope model class
                var horoscope = JsonConvert.DeserializeObject<Horoscope>(json);
                // Return the Horoscope object as a view
                return View(horoscope);
            }
        }
    }
}

