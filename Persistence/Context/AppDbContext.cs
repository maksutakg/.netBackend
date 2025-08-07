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
       
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasIndex(u=>u.Mail).IsUnique();


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
                new Mahalle { Id = 9, Name = "Gayrettepe", Ilce = "Beşiktaş" },
                new Mahalle { Id = 10, Name = "Konaklar", Ilce = "Beşiktaş" },
                new Mahalle { Id = 11, Name = "Kuruçeşme", Ilce = "Beşiktaş" },
                new Mahalle { Id = 12, Name = "Kültür", Ilce = "Beşiktaş" },
                new Mahalle { Id = 13, Name = "Levazım", Ilce = "Beşiktaş" },
                new Mahalle { Id = 14, Name = "Levent", Ilce = "Beşiktaş" },
                new Mahalle { Id = 15, Name = "Mecidiye", Ilce = "Beşiktaş" },
                new Mahalle { Id = 16, Name = "Muradiye", Ilce = "Beşiktaş" },
                new Mahalle { Id = 17, Name = "Nispetiye", Ilce = "Beşiktaş" },
                new Mahalle { Id = 18, Name = "Ortaköy", Ilce = "Beşiktaş" },
                new Mahalle { Id = 19, Name = "Sinanpaşa", Ilce = "Beşiktaş" },
                new Mahalle { Id = 20, Name = "Türkali", Ilce = "Beşiktaş" },
                new Mahalle { Id = 21, Name = "Ulus", Ilce = "Beşiktaş" },
                new Mahalle { Id = 22, Name = "Vişnezade", Ilce = "Beşiktaş" },
                new Mahalle { Id = 23, Name = "Yıldız", Ilce = "Beşiktaş" }
            );


        }
    }
}
