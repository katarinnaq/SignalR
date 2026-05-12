
using Microsoft.AspNetCore.Identity;
namespace ProjekatSignalR.Models

{
    public class Korisnik:IdentityUser
    {
        public string? Ime {  get; set; }
        public string? Prezime { get; set; }
        public string? KorisnickoIme { get; set; }
        public DateTime? DatumRegistracije { get; set; } = DateTime.UtcNow;


    }
}
