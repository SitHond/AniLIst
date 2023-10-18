using API.Controllers;
using API.interfaces;
using Microsoft.AspNetCore.Mvc;
using site.Models;

namespace site.Controllers
{
    [ApiController]
    [Route("anime")]
    public class AnimeController : Controller
    {
        private IAnimeRepository _animeRepository;
        private readonly AppDbContext _context;
        public AnimeController(IAnimeRepository repository)
        {
            _animeRepository = repository; 
        }

        [HttpGet]
        [Route("list")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<Anime>))]
        public async Task<IActionResult> Animes([FromQuery] int PageNumber, [FromQuery] int PageSize)
        {
            return Json(await _animeRepository.GetAnimeAsync( PageNumber, PageSize));
        }

        [HttpGet]
        [Route("")]
        [ProducesResponseType(200)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetAnimeByUrlAsync([FromQuery]string Url)
        {
            var anime = await _animeRepository.GetAnimeByUrlAsync(Url);
            if (anime == null)
            {
                return BadRequest("anime_with_url_not_found");
            }

            return Json(anime);
        }

        [HttpPost]
        [Route("add-anime")]
        [ProducesResponseType(201)] // Возвращайте 201 Created при успешном создании
        [ProducesResponseType(400)] // Возвращайте 400 Bad Request при ошибке валидации
        public async Task<IActionResult> CreateAnime([FromBody] Anime anime)
        {

            if (anime == null)
            {
                return BadRequest("Аниме не может быть пустым.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                // Проверяем наличие дубликата по имени
                var existingAnime = await _animeRepository.GetAnimeByNameAsync(anime.Name);

                if (existingAnime.Count > 0)
                {
                    return BadRequest("Аниме с таким именем уже существует.");
                }

                // Создаем экземпляр аниме и добавляем его в контекст базы данных
                var newAnime = new Anime
                {
                    Name = anime.Name,
                    Russian = anime.Russian,
                    ImageOriginal = anime.ImageOriginal,
                    ImagePreview = anime.ImagePreview,
                    ImageX96 = anime.ImageX96,
                    ImageX48 = anime.ImageX48,
                    Url = anime.Url,
                    Kind = anime.Kind,
                    Score = anime.Score,
                    Status = anime.Status,
                    Episodes = anime.Episodes,
                    EpisodesAired = anime.EpisodesAired,
                    AiredOn = anime.AiredOn,
                    ReleasedOn = anime.ReleasedOn
                    // Другие свойства аниме
                };

                await _animeRepository.InsertAnimeAsync(newAnime);
                await Console.Out.WriteLineAsync(newAnime.Id.ToString());
                // Возвращаем успешный результат с созданным аниме
                return CreatedAtAction("GetAnimeById", new { id = newAnime.Id }, newAnime);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }
    }
}
