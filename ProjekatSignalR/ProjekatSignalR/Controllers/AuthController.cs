using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using ProjekatSignalR.Models;

namespace ProjekatSignalR.Controllers
{
    [ApiController]
    [Route("api/[controller]")] // (npr. POST /api/Auth/register)
    public class AuthController : ControllerBase
    {
        // UserManager sluzi za upravljanje korisnicima
        private readonly UserManager<Korisnik> _userManager;
        // SignInManager sluzi za login/logout funkcionalnosti
        private readonly SignInManager<Korisnik> _signInManager;

        // Konstruktor
        public AuthController(UserManager<Korisnik> userManager, SignInManager<Korisnik> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        // Registracija za novog korisnika
        // POST /api/Auth/register
        [HttpPost("register")]
        public async Task <IActionResult> Register(RegisterModel model)
        {
            // Kreiranje novog korisnika na osnovu podataka iz modela
            var user = new Korisnik
            {
                UserName = model.UserName,
                Email = model.Email
            };

            // Kreiranje korisnika u Identity sistemu sa zadatim passwordom
            var result = await _userManager.CreateAsync(user, model.Password);

            // Ako je kreiranje uspesno vracamo OK
            if (result.Succeeded)
            {
                return Ok("Korisnik registrovan");
            }

            // Ako nije vracamo gresku
            return BadRequest(result.Errors);
        }

        // Logovanje korisnika
        // POST /api/Auth/login
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginModel model)
        {
            // Provera podataka za login
            var result = await _signInManager.PasswordSignInAsync(
                    model.UserName, // ime
                    model.Password, // sifra
                    false,          // ne "remember me"
                    false);         // ne blokiraj korisnika nakon neuspesnih pokusaja

            // Ako je ulogovan uspesno vracamo ok
            if (result.Succeeded)
            {
                return Ok("Uspesan login");
            }

            // Ako nije vracamo gresku
            return BadRequest("Pogresni rezultati");
        }
    }

    // Model za registraciju
    public class RegisterModel
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }

    // Model za login   
    public class LoginModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
