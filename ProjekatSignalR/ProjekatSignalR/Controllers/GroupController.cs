using Microsoft.AspNetCore.Mvc;
using ProjekatSignalR.Data;
using ProjekatSignalR.Models;
using System.Security.Claims;

namespace ProjekatSignalR.Controllers
{
    [ApiController]
    [Route("api/[controller]")] // (npr. POST /api/Group/create)
    public class GroupController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public GroupController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Ucitavanje grupa
        // GET /api/Group
        [HttpGet]
        public IActionResult GetGroups()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var groups = _context.ClanoviGrupe
                .Where(x => x.KorisnikId == userId)
                .Select(x => new { x.Grupa.Id, x.Grupa.Naziv })
                .Distinct()
                .ToList();

            return Ok(groups);
        }

        // Ucitavanje istorije poruka
        // GET /api/Group/messages/{groupId}
        [HttpGet("messages/{groupId}")]
        public IActionResult GetGroupMessages(int groupId)
        {
            var messages = _context.GrupnePoruke
                .Where(m => m.GrupaId == groupId)
                .Select(m => new { m.PosiljalacId, m.Poruka, m.DatumSlanja })
                .ToList();

            return Ok(messages);
        }
    }
}
