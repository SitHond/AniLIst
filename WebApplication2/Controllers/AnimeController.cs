using API.Controllers;
using API.interfaces;
using Microsoft.AspNetCore.Mvc;

namespace site.Controllers
{
    [ApiController]
    [Route("anime")]
    public class AnimeController : Controller
    {
        private IAnimeRepository _animeRepository;
        public AnimeController(IAnimeRepository repository)
        {
            _animeRepository = repository; 
        }

        [HttpGet]
        [Route("")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> Animes(int PageNumber, int PageSize)
        {
            return Json(await _animeRepository.GetAnimeAsync( PageNumber, PageSize));
        }
        [HttpGet("{Url}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetAnimeByUrlAsync(string Url)
        {
            var anime = await _animeRepository.GetAnimeByUrlAsync(Url);
            if (anime == null)
            {
                return NotFound();
            }

            return Json(anime);
        }
    }
}
