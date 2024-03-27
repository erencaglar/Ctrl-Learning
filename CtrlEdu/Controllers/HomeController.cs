using CtrlEdu.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace CtrlEdu.Controllers
{
   // [Authorize]

    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            // Check if the user is authenticated
            if (User.Identity.IsAuthenticated)
            {
                // Retrieve the user's ID claim
                var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);

                if (userIdClaim != null)
                {
                    // You can access the user's ID as a string
                    string userId = userIdClaim.Value;

                    // Now, you can use userId in your controller logic
                }
            }

            // Rest of your Index action logic
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}