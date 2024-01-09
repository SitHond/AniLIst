using API.Models;
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
        public DbSet<User> users { get; set; }
        public DbSet<AccessToken> AccessTokens { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Anime>().ToTable(t => t.HasCheckConstraint(nameof(Anime.Score), $"{nameof(Anime.Score)} < 10"));
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL("Server=95.163.234.141;Database=ShikiDB;User Id=sithond;Password=NotauzaKien7;Charset=utf8mb4;");
        }
    }
}
