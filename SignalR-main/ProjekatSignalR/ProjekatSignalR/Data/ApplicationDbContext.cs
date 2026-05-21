using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProjekatSignalR.Models;

namespace ProjekatSignalR.Data
{
    public class ApplicationDbContext : IdentityDbContext<Korisnik>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<PrivatniChat> PrivatnePoruke { get; set; }
        public DbSet<Grupa> Grupe { get; set; }
        public DbSet<ClanoviGrupe> ClanoviGrupe { get; set; }
        public DbSet<GrupnaPoruka> GrupnePoruke { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<PrivatniChat>()
                .HasOne(p => p.Posiljalac)
                .WithMany()
                .HasForeignKey(p => p.PosiljalacId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<PrivatniChat>()
                .HasOne(p => p.Primalac)
                .WithMany()
                .HasForeignKey(p => p.PrimalacId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}