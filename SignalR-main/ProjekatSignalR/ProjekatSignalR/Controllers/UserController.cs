using Microsoft.AspNetCore.Mvc;
using ProjekatSignalR.Data;
using System.Security.Claims;

namespace ProjekatSignalR.Controllers
{
    [ApiController] // oznaka da je Api kontroler
    [Route("api/[controller]")] // ruta koja se koristi za pristup ovom kontroleru (npr. GET /api/User)
    public class UserController : ControllerBase
    {
        // DbContext preko kojeg pristupamo bazi podataka
        private readonly ApplicationDbContext _context;

        // Konstruktor koji koristi DbContext
        public UserController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET endpoint koji vraca listu svih korisnika
        [HttpGet]
        public IActionResult GetUsers()
        {
            // Dohvatanje svih korisnika iz Identity tabele User
            var users = _context.Users
                .Where(u => u.UserName != User.Identity.Name) // izaci sebe iz liste
                .Select(u => new { u.Id, u.UserName})
                .ToList();

            // vracamo listu korisnika sa statusom OK
            return Ok(users);
        }

        // Ucitavanje istorije poruka
        // GET /api/User/messages/{otherUserId}
        [HttpGet("messages/{otherUserId}")]
        public IActionResult GetMessages(string otherUserId)
        {
            var currentUserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            Console.WriteLine("TrenutniUserId: " + currentUserId);

            if(string.IsNullOrEmpty(currentUserId))
            {
                return Ok(new List<object>()); // vraca prazan niz
            }

            var messages = _context.PrivatnePoruke
                .Where(p => (p.PosiljalacId == currentUserId && p.PrimalacId == otherUserId) ||
                            (p.PosiljalacId == otherUserId && p.PrimalacId == currentUserId))
                .OrderBy(p => p.PoslatoU)
                .Select(p => new { p.PosiljalacId, p.Sadrzaj })
                .ToList();

            return Ok(messages);
        }
    }
}
