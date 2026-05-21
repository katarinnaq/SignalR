using Microsoft.AspNetCore.SignalR;
using ProjekatSignalR.Data;
using ProjekatSignalR.Models;
using System.Text.RegularExpressions;

namespace ProjekatSignalR.Hubs
{
    // Hub predstavlja centralnu racku kominikacije.
    // Svi korisnici komuniciraju preko njega.
    public class ChatHub : Hub
    {
        // DbContext koristimo da pristupimo bazi
        private readonly ApplicationDbContext _context;

        public ChatHub(ApplicationDbContext context)
        {
            _context = context;
        }

        // PRIVATNE PORUKE
        // Ova metoda salje privatnu poruku izmedju dva korisnika
        // senderId = ko salje poruku
        // receiberId = ko prima poruku
        // message = sadrzaj poruke
        public async Task SendPrivateMessage(string senderId, string receiverId, string message)
        {
            try
            {
                Console.WriteLine("Sender: sender=Id{senderId}, receiverId={receiverId}, message={message}");
                // Provera polja da li su null
                if (string.IsNullOrEmpty(senderId) || string.IsNullOrEmpty(receiverId)
                    || string.IsNullOrEmpty(message))
                {
                    throw new Exception("ReceverId nije validan.");
                }

               // Pravimo objekat poruke koji ce biti sacuvan u bazi
               var poruka = new PrivatniChat
               {
                   PosiljalacId = senderId,
                   PrimalacId = receiverId,
                   Sadrzaj = message,
                   PoslatoU = DateTime.Now
               };

                // Dodajemo poruku u bazu
                _context.PrivatnePoruke.Add(poruka);

                // Cuvamo promene u bazi
                await _context.SaveChangesAsync();

                // Saljemo poruku korisniku koji prima poruku
                // ReceivePrivateMessage ce frontend slusati
                await Clients.User(receiverId).SendAsync("ReceivePrivateMessage", senderId, message);

                // Saljemo poruku i posiljaocu kako bi je video odmah u svom chatu
                await Clients.Caller.SendAsync("ReceivePrivateMessage", senderId, message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("greska: " + ex.Message);
                throw;
            }

        }

        public override async Task OnConnectedAsync()
        {
            var userId = Context.UserIdentifier;
            var groups = _context.ClanoviGrupe
                .Where(x => x.KorisnikId == userId)
                .Select(x =>  x.GrupaId)
                .ToList();

            foreach(var groupId in groups)
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, groupId.ToString());
            }

            await base.OnConnectedAsync();
        }

        // GRUPNE PORUKE
        // Salje poruku svim korisnicima u grupi
        public async Task SendGroupMessage(int groupId, string senderId, /*string groupName,*/ string message)
        {
            var grupnaPoruka = new GrupnaPoruka
            {
                GrupaId = groupId,
                PosiljalacId = senderId,
                Poruka = message,
                DatumSlanja = DateTime.Now
            };

            _context.GrupnePoruke.Add(grupnaPoruka);

            await _context.SaveChangesAsync();

            // Saljemo poruku SVIM clanovima grupe
            await Clients.Group(groupId.ToString()).SendAsync("ReceiveGroupMessage", senderId, message);
        }
    }
}
