using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;


namespace Persistence.Context
{
    public class AppDbContext : DbContext
    {
         public DbSet<User> Users { get; set; }
         public DbSet<Note> Notes { get; set; }
        public DbSet<Mahalle> mahalleler { get; set; }
       
        public AppDbContext(DbContextOptions options) : base(options)
        {

        }
        protected AppDbContext()
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasMany(u => u.Notes)
            .WithOne(u => u.user).HasForeignKey(u => u.UserId).IsRequired();

            modelBuilder.Entity<Mahalle>().HasMany(u => u.Notes)
            .WithOne(u => u.mahalle).HasForeignKey(u => u.MahalleId).IsRequired();

            modelBuilder.Entity<Mahalle>().HasData(
         new Mahalle { Id = 1, Name = "Abbasağa", Ilce = "Beşiktaş" },
         new Mahalle { Id = 2, Name = "Akat", Ilce = "Beşiktaş" },
         new Mahalle { Id = 3, Name = "Arnavutköy", Ilce = "Beşiktaş" },
         new Mahalle { Id = 4, Name = "Balmumcu", Ilce = "Beşiktaş" },
         new Mahalle { Id = 5, Name = "Bebek", Ilce = "Beşiktaş" },
         new Mahalle { Id = 6, Name = "Cihannüma", Ilce = "Beşiktaş" },
         new Mahalle { Id = 7, Name = "Dikilitaş", Ilce = "Beşiktaş" },
         new Mahalle { Id = 8, Name = "Etiler", Ilce = "Beşiktaş" },
         new Mahalle { Id = 9, Name = "Gayrettepe", Ilce = "Beşiktaş" }
  
                );

        }
    }
}
