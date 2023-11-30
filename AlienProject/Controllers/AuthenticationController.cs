using AlienProject.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AlienProject.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly AlienDbContext _context;
        public AuthenticationController(AlienDbContext context) {
            _context = context;
        }

        [HttpGet("login")]
        public IActionResult Login() {
            return View();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Validate(string email,string password)
        {
            var human = _context.Humans
               .FirstOrDefault(u => u.Email == email && u.Password == password);

            var alien = _context.Aliens
                .FirstOrDefault(u => u.Email == email && u.Password == password);

            if(human!= null) {
                var claims = new List<Claim>();
                claims.Add(new Claim("email", email));
                claims.Add(new Claim(ClaimTypes.Email,email));
                claims.Add(new Claim(ClaimTypes.Role, "Human"));
                claims.Add(new Claim(ClaimTypes.NameIdentifier, email));
                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
                await HttpContext.SignInAsync(claimsPrincipal);

                return RedirectToAction("Humans","Human");
            }
            else if(alien!= null)
            {
                var claims = new List<Claim>();
                claims.Add(new Claim("email", email));
                claims.Add(new Claim(ClaimTypes.Email, email));
                claims.Add(new Claim(ClaimTypes.Role, "Alien"));
                claims.Add(new Claim(ClaimTypes.NameIdentifier, email));
                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
                await HttpContext.SignInAsync(claimsPrincipal);
                return RedirectToAction("Aliens","Alien");
            }
            return RedirectToAction("Index","Home");
        }

        public async Task<IActionResult> Logout() {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Login");
        }
    }
}
