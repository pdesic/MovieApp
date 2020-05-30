using Microsoft.AspNetCore.Mvc;

namespace MovieApp.Controllers
{
    public class OMDBMoviesController : Controller
    {
        // GET
        public IActionResult Index()
        {
            return View();
        }
    }
}