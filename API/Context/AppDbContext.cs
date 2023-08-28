using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using site.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Controllers
{

    public class AppDbContext : DbContext
    {       
        public DbSet<Anime> animes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Anime>().ToTable(t => t.HasCheckConstraint(nameof(Anime.Score), $"{nameof(Anime.Score)} < 10"));
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL("Server=31.31.198.144;Database=u1317360_ShikiDB;User Id=u1317360_sithond;Password=wS8wJ6uT1cyI3tV8;");
        }
    }
}
