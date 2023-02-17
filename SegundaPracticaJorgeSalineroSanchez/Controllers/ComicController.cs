using Microsoft.AspNetCore.Mvc;
using SegundaPracticaJorgeSalineroSanchez.Models;
using SegundaPracticaJorgeSalineroSanchez.Repositories;

namespace SegundaPracticaJorgeSalineroSanchez.Controllers
{
    public class ComicController : Controller
    {
        private IRepositoryComic repo;
        public ComicController(IRepositoryComic repo)
        {
            this.repo = repo;
        }
        public IActionResult Index()
        {
            List<Comic> comics = this.repo.GetComics();
            return View(comics);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Comic comic)
        {
            this.repo.InsertarComic(comic);
            return RedirectToAction("Index");
        }

    }
}
