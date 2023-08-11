using Microsoft.EntityFrameworkCore;
using site.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Controllers
{
    public class AppDbContext : DbContext
    {
        public DbSet<Anime> animes { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Anime>().ToTable(t => t.HasCheckConstraint(nameof(Anime.Score), $"{nameof(Anime.Score)} < 10"));
        }
    }
}
