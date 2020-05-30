using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MovieApp.Controllers
{
    [Authorize]
    public class OMDBMoviesController : Controller
    {
        // GET
        public IActionResult Index()
        {
            return View();
        }
    }
}