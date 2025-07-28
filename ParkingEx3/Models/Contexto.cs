using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ParkingEx3.Models
{
    public class Contexto : DbContext                                                                                                               
    {
        public Contexto(DbContextOptions<Contexto> options)
            : base(options)
        {
        }

        public DbSet<Usuarios> Usuarios { get; set; }
        public DbSet<Plazas> Plazas { get; set; } 
        public DbSet<Reservas> Reservas { get; set; }

        
    }
}
