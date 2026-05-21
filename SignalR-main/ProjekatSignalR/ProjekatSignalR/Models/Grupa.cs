namespace ProjekatSignalR.Models
{
    public class Grupa
    {
        public int Id { get; set; } // id grupe
        public string Naziv { get; set; } // naziv grupe
        public ICollection<GrupnaPoruka> Poruke { get; set; } // lista svih poruka napisanih u grupi!!
    }
}
