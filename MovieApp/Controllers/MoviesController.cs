﻿using System;
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
        private IHostingEnvironment _env;

        public MoviesController(MovieService movieService, GenreService genreService, IHostingEnvironment environment)
        {
            _movieService = movieService;
            _genreService = genreService;
            _env = environment;
        }

        [HttpGet]
        public ActionResult<Movie> Index()
        {
            var movies = _movieService.Get();

            return View(movies);
        }

        public ActionResult Details(string id)
        {
            return Content("That is movie ID = " + id.ToString() + "It's work AAAAAA");
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
            
            var viewModel = new MovieFormViewModel
            {
                Genre = _genreService.GetFirst(),
                Movie = movie
            };    

            return View(viewModel);
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


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(Movie movie, string id)
        {
            var updateMovie = _movieService.Get(id);

            if (updateMovie == null)
            {
                return NotFound();
            }
            
            movie.Id = id;

            _movieService.Update(id, movie);

            return RedirectToAction("Index", "Movies");
        }

        [HttpDelete]
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
