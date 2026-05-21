using Microsoft.AspNetCore.Mvc;
using ProjekatSignalR.Data;
using ProjekatSignalR.Models;

namespace ProjekatSignalR.Controllers
{
    [ApiController]
    [Route("api/[controller]")] // (npr. POST /api/CreateGroup/create)
    public class CreateGroupController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CreateGroupController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Kreiranje nove grupe
        // POST /api/CreateGroup/create
        [HttpPost("create")]
        public async Task<IActionResult> CreateGroup([FromBody] KreiranjeGrupe kreiranjegrupa)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Kreiranje grupa
            var grupa = new Grupa
            {
                Naziv = kreiranjegrupa.Naziv
            };

            _context.Grupe.Add(grupa);

            await _context.SaveChangesAsync();

            var sviKorisnici = kreiranjegrupa.KorisniciId.Distinct();

            // Dodavanje clanova
            foreach(var korisnikId in /*kreiranjegrupa.KorisniciId*/ sviKorisnici)
            {
                var clan = new ClanoviGrupe
                {
                    GrupaId = grupa.Id,
                    KorisnikId = korisnikId
                };

                _context.ClanoviGrupe.Add(clan);
            }

            await _context.SaveChangesAsync();

            return Ok(new {
                grupa.Id,
                grupa.Naziv
            });
        }

    }
}
