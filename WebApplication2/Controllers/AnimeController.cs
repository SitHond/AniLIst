using API.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace site.Controllers

{
    public class AnimeController : Controller
    {
        private readonly AppDbContext _dbContext;

        public AnimeController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            var animeList = _dbContext.Anime.ToList();
            return View(animeList);
        }
    }
}
