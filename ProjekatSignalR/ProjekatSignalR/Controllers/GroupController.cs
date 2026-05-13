using Microsoft.AspNetCore.Mvc;
using ProjekatSignalR.Data;
using ProjekatSignalR.Models;

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

        // Kreiranje nove grupe
        // POST /api/Group/create
        [HttpPost("create")]
        public async Task <IActionResult> CreateGroup(Grupa grupa)
        {
            // Dodajemo novu grupu
            _context.Grupe.Add(grupa);

            // Cuvamo promene u bazi
            await _context.SaveChangesAsync();

            // Vracamo kreiranu grupu
            return Ok(grupa);
        }

        // Dodavanje novog clana u grupu
        // POST /api/Group/add-member
        [HttpPost("add-member")]
        public async Task<IActionResult> AddMember(ClanoviGrupe clan)
        {
            // Dodajemo clana
            _context.ClanoviGrupe.Add(clan);

            // Cuvamo promene u bazi
            await _context.SaveChangesAsync();

            // Vracamo clana grupe kao rezultat
            return Ok(clan);
        }
    }
}
