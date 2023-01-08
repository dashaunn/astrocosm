using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Astrocosm.Data;
using Astrocosm.Models;
using Dapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;


namespace Astrocosm.Controllers
{
    public class SubmitDOBController : Controller
    {
        // Allow the controller to access and manage users/user information in the application
        private readonly UserManager<AppUser> _userManager;

        public SubmitDOBController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        // Display the Sun Sign Calculator
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult SubmitDOB(DateTime DOB)
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
                    // Save the user's birthdate (passed in as DOB)
                    user.DOB = DOB;
                    _userManager.UpdateAsync(user).Wait();
                }

                // Send the user to a page where they can verify the DOB that is saved to the database
                return RedirectToAction("Index", "VerifyDOB");
            }

        }
    }
