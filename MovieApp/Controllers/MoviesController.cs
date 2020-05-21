using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MovieApp.Models;
using MovieApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace MovieApp.Controllers
{
    public class MoviesController : Controller
    {
        private readonly MovieService _movieService;

        public MoviesController(MovieService movieService)
        {
            _movieService = movieService;
        }
        public ActionResult<Movie> Index()
        {
            var movies = _movieService.Get();

            return View(movies);
        }

        public ViewResult CreateForm()
        {
            return View("");
        }

        public ActionResult UpdateForm(string id)
        {
            var movie = _movieService.Get(id);

            if(movie == null)
            {
                return NotFound();
            }



            return View("");
        }

        [HttpPost]
        public ActionResult<Movie> Create(Movie movie)
        {
            _movieService.Create(movie);

            return RedirectToAction("Index","Movies");
        }

        [HttpPost]
        public ActionResult<Movie> Update(string id, Movie movieIn)
        {
            var updateMovie = _movieService.Get(id);

            if (updateMovie == null)
            {
                return NotFound();
            }

            _movieService.Update(id, movieIn);

            return RedirectToAction("Index", "Movies");
        }

        [HttpPost]
        public ActionResult<Movie> Delete(string id)
        {
            var deleteMovie = _movieService.Get(id);

            if(deleteMovie == null)
            {
                return NotFound();
            }

            _movieService.Remove(deleteMovie.Id);

            return RedirectToAction("Index", "Movies");
        }
    }
}
