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
        [HttpGet("register")]
        public IActionResult Register() {
            return View();
        }

        [HttpPost("register")]
        public IActionResult Register(string name,string email,DateTime birth,string password) {
            Alien alien = new();
            Human human = new();
            if (email.Contains("alien"))
            {
                var lastElement = _context.Aliens.OrderByDescending(x => x.AlienId).FirstOrDefault();
                alien.AlienId = lastElement.AlienId + 1;
                alien.Name = name;
                alien.Email = email;
                alien.BirthDate = birth;
                alien.Password = password;
                _context.Aliens.Add(alien);
                _context.SaveChanges();
                return RedirectToAction("Login");
            }
            else {
                var lastElement = _context.Humans.OrderByDescending(x => x.HumanId).FirstOrDefault();
                human.HumanId = lastElement.HumanId + 1;
                human.Name = name;
                human.Email = email;
                human.BirthDate = birth;
                human.Password = password;
                _context.Humans.Add(human);
                _context.SaveChanges();
                return RedirectToAction("Login");
            }

            return RedirectToAction("Index","Home");
        }
    }
}
