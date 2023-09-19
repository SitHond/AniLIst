using site.Models;

namespace API.interfaces
{
    public interface IAnimeRepository
    {
        Task<List<Anime>> GetAnimeAsync(int PageNumber, int PageSize);
        Task<bool> InsertAnimeAsync(Anime newAnime);
        Task<List<Anime>> GetAnimeByUrlAsync(string Url);
        Task<bool> UpdateAnimeAsync(Guid uid);
        Task<bool> DeleteAnimeAsync(Guid uid);
    }
}
