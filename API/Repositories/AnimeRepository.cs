using API.Controllers;
using API.interfaces;
using Microsoft.EntityFrameworkCore;
using site.Models;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Net.Http.Json;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace API.Repositories
{
    public class AnimeRepository : IAnimeRepository
    {
        private AppDbContext _context;

        public AnimeRepository(AppDbContext context)
        {
            _context = context;        
        }

        public Task<bool> DeleteAnimeAsync(Guid uid)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Anime>> GetAnimeAsync(int PageNumber, int PageSize)
        {
            return await _context.animes.Skip(PageNumber * PageSize).Take(PageSize).ToListAsync();

        }
        public async Task<List<Anime>> GetAnimeByUrlAsync(string Url)
        {
            return await _context.animes.Where(a => a.Url == Url).ToListAsync();
        }

        public async Task<bool> InsertAnimeAsync(Anime newAnime)
        {
            await _context.animes.AddAsync(newAnime);
            return await _context.SaveChangesAsync() > 0;
        }

        public Task<bool> UpdateAnimeAsync(Guid uid)
        {
            throw new NotImplementedException();
        }
    }
}
