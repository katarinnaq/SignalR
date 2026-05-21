namespace ProjekatSignalR.Models
{
    public class GrupnaPoruka
    {
        public int Id { get; set; } // id poruke
        public int GrupaId { get; set; }
        public Grupa Grupa { get; set; } // poveizvanje sa grupom
        public string PosiljalacId { get; set; }
        public Korisnik Posiljalac { get; set; }
        public string Poruka { get; set; }
        public DateTime DatumSlanja { get; set; } = DateTime.Now;
    }
}
