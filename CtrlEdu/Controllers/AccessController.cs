using CtrlEdu.Data;
using CtrlEdu.Models;
using CtrlEdu.Services; // Import the UserService namespace
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CtrlEdu.Controllers
{
    public class AccessController : Controller
    {
        private readonly UserService _userService;
        private readonly ApplicationDbContext _context; // Add the database context

        public AccessController(UserService userService, ApplicationDbContext context)
        {
            _userService = userService;
            _context = context; // Inject the database context
        }



        public IActionResult Login()
        {
            ClaimsPrincipal claimUser = HttpContext.User;
            if (claimUser.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(VMlogin modelLogin)
        {
            var user = await _userService.GetUserByEmailAsync(modelLogin.Email);

            if (user != null && user.Password == modelLogin.Password)
            {
                // Set the role claim based on the user's 'Rol' attribute from the database
                string role = user.Rol;

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, user.UserID.ToString()), // User ID as a claim
                    new Claim(ClaimTypes.NameIdentifier, user.Email),
                    new Claim(ClaimTypes.NameIdentifier, user.UserName),
                    new Claim(ClaimTypes.Role, role) // Set the role claim based on the 'Rol' attribute
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                var authProperties = new AuthenticationProperties
                {
                    AllowRefresh = true,
                    IsPersistent = modelLogin.KeepLoggedIn
                };

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    authProperties);

                return RedirectToAction("Index", "Home");
            }

            ViewData["ValidateMessage"] = "User not found or incorrect password";
            return View();
        }

        // GET: /Access/SignUp
        public IActionResult SignUp()
        {
            // Check if the user is already authenticated and redirect if necessary
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        // POST: /Access/SignUp
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignUp(UserModel userModel)
        {
            if (ModelState.IsValid)
            {
                // Check if the email already exists in the database
                var existingUser = await _userService.GetUserByEmailAsync(userModel.Email);
                if (existingUser != null)
                {
                    ModelState.AddModelError(string.Empty, "Email already exists. Please use a different email.");
                    return View(userModel);
                }

                // Add the new user to the database
                _context.Users.Add(userModel);
                await _context.SaveChangesAsync();

                // Redirect the user to the login page or perform additional actions
                return RedirectToAction("Login", "Access");
            }

            // If there are validation errors, display them on the sign-up page
            return View(userModel);
        }

    }
}
