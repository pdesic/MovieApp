using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MovieApp.Models;
using MovieApp.Services;
using Microsoft.AspNetCore.Mvc;
using MovieApp.ViewModels;

namespace MovieApp.Controllers
{
    public class MoviesController : Controller
    {
        private readonly MovieService _movieService;
        private readonly GenreService _genreService;

        public MoviesController(MovieService movieService, GenreService genreService)
        {
            _movieService = movieService;
            _genreService = genreService;
        }

        [HttpGet]
        public ActionResult<Movie> Index()
        {
            var movies = _movieService.Get();

            return View(movies);
        }



        public ActionResult Details(string id)
        {
            var movie = _movieService.Get(id);
            return View(movie);
        }

        public ActionResult<Genre> CreateForm()
        {

            var viewModel = new MovieFormViewModel
            {
                Genre = _genreService.GetFirst(),
                Movie = new Movie()
            };    

            return View(viewModel);
        }


    

    public ActionResult UpdateForm(string id)
        {
            var movie = _movieService.Get(id);

            if(movie == null)
            {
                return NotFound();
            }

            var viewModel = new MovieFormViewModel()
            {
                Genre = _genreService.GetFirst(),
                Movie = movie
            };

            return View(movie);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult<Movie> Create(Movie movie)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new MovieFormViewModel
                {
                    Genre = _genreService.GetFirst(),
                    Movie = new Movie()
                };   

                return View("CreateForm", viewModel);
            }
            
            _movieService.Create(movie);

            return RedirectToAction("Index","Movies");
        }

        [HttpPut]
        [ValidateAntiForgeryToken]
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

        [HttpDelete]
        [ValidateAntiForgeryToken]
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
