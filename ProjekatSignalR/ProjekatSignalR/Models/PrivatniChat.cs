using ProjekatSignalR.Models;

public class PrivatniChat
{
    public int Id { get; set; } // id poruke
    public string PosiljalacId { get; set; } // id posiljalaca
    public Korisnik Posiljalac { get; set; } // posiljalac
    public string PrimalacId { get; set; } // primalac id
    public Korisnik Primalac { get; set; } // primalac
    public string Sadrzaj { get; set; } // sadrzaj poruke
    public DateTime PoslatoU { get; set; } = DateTime.Now; // kada je poruka poslata
}