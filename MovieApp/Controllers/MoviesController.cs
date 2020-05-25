using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using MovieApp.Models;
using MovieApp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
<<<<<<< HEAD

=======
            
>>>>>>> 7b74fc708ab781795770b79a7376735e99fd0ff1
            return View(viewModel);
        }


    

    public ActionResult UpdateForm(string id)
        {
            var movie = _movieService.Get(id);

            if(movie == null)
            {
                return NotFound();
            }
            
            var viewModel = new MovieFormViewModel
            {
                Genre = _genreService.GetFirst(),
                Movie = movie
            };    

<<<<<<< HEAD
            var viewModel = new MovieFormViewModel()
            {
                Genre = _genreService.GetFirst(),
                Movie = movie
            };

            return View(movie);

=======
            return View(viewModel);
>>>>>>> 7b74fc708ab781795770b79a7376735e99fd0ff1
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Movie movie, IFormFile image)
        {
            // If image uploaded, make image name unique and store into "\wwwroot\images" folder,
            // otherwise set validation error message
            if (image != null && image.Length > 0)
            {
                string uniqueImageName = Guid.NewGuid().ToString() + "_" + image.FileName;

                string imageFullPath = System.IO.Directory.GetCurrentDirectory() + @"\wwwroot\images\" + uniqueImageName;
                
                using (var stream = System.IO.File.Create(imageFullPath))
                {
                    await image.CopyToAsync(stream);
                }

                movie.ImagePath = uniqueImageName;

            }
            else
            {
                ModelState.AddModelError("Movie.ImagePath", "Please upload image");
            }
            
            // Check if has any validation error
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

<<<<<<< HEAD
        [HttpPut]
        [ValidateAntiForgeryToken]
        public ActionResult<Movie> Update(string id, Movie movieIn)
=======

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(Movie movie, string id, IFormFile image)
>>>>>>> 7b74fc708ab781795770b79a7376735e99fd0ff1
        {
                        
            var updateMovie = _movieService.Get(id);

            if (updateMovie == null)
            {
                return NotFound();
            }
            
            // If image uploaded, make image name unique and store into "\wwwroot\images" folder,
            // otherwise set validation error message
            if (image != null && image.Length > 0)
            {
                string uniqueImageName = Guid.NewGuid().ToString() + "_" + image.FileName;

                string imageFullPath = System.IO.Directory.GetCurrentDirectory() + @"\wwwroot\images\" + uniqueImageName;
                
                using (var stream = System.IO.File.Create(imageFullPath))
                {
                    await image.CopyToAsync(stream);
                }

                movie.ImagePath = uniqueImageName;
            }
            else
            {
                movie.ImagePath = updateMovie.Id;
            }

            
            // Check if has any validation error
            if (!ModelState.IsValid)
            {
                var viewModel = new MovieFormViewModel
                {
                    Genre = _genreService.GetFirst(),
                    Movie = new Movie()
                };   
            
                return View("UpdateForm", viewModel);
            }
            
            
            _movieService.Update(id, movie);

            return RedirectToAction("Index", "Movies");
        }

<<<<<<< HEAD
        [HttpDelete]
        [ValidateAntiForgeryToken]
=======
        [HttpPost]
        //TODO add TOKEN
>>>>>>> 7b74fc708ab781795770b79a7376735e99fd0ff1
        public ActionResult<Movie> Delete(string id)
        {
            var deleteMovie = _movieService.Get(id);

            if(deleteMovie == null)
            {
                return NotFound();
            }

            _movieService.Remove(id);

            return RedirectToAction("Index", "Movies");
        }
    }
}
