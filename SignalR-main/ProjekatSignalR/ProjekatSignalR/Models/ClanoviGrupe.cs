namespace ProjekatSignalR.Models
{
    public class ClanoviGrupe 
    {
        public int Id { get; set; }
        public int GrupaId { get; set; } 
        public Grupa Grupa { get; set; } // povezivanje grupe (id, naziv)
        public string KorisnikId { get; set; } // id korisnika iz identity tabele
        public Korisnik Korisnik { get; set; } // povezivanje korisnika (ime, prezime, korisnicko ime)
    }
}
