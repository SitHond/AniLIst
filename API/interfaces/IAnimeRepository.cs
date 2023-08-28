using site.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.interfaces
{
    public interface IAnimeRepository
    {
        Task<List<Anime>> GetAnimeAsync(int PageNumber, int PageSize);
        Task<bool> InsertAnimeAsync(string SearchParam);
        Task<List<Anime>> GetAnimeByUrlAsync(string Url);
        Task<bool> UpdateAnimeAsync(Guid uid);
        Task<bool> DeleteAnimeAsync(Guid uid);
    }
}
