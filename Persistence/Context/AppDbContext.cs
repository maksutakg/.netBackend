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
       
        public AppDbContext(DbContextOptions options) : base(options)
        {

        }
        protected AppDbContext()
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
      
            modelBuilder.Entity<User>().HasMany(u => u.Notes).WithOne(u => u.user).HasForeignKey(u => u.UserId).IsRequired();
        }
    }
}
