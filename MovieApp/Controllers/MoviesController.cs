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
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;

namespace MovieApp.Controllers
{
    [Authorize]
    public class MoviesController : Controller
    {
        private readonly MovieService _movieService;
        private readonly GenreService _genreService;
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;

        public MoviesController(MovieService movieService, GenreService genreService, SignInManager<User> signInManager, UserManager<User> userManager)
        {
            _movieService = movieService;
            _genreService = genreService;
            _signInManager = signInManager;
            _userManager = userManager;

        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult<Movie> Index()
        {
            var movies = _movieService.Get();

            return View(movies);
        }

        [HttpGet]
        public ActionResult Details(string id)
        {
            var movie = _movieService.Get(id);
            return View(movie);
        }

        [HttpGet]
        public ActionResult<Genre> CreateForm()
        {

            var viewModel = new MovieFormViewModel
            {
                Genre = _genreService.GetFirst(),
                Movie = new Movie()
            };

            return View(viewModel);
        }

        [HttpGet]
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

            movie.Rating = Array.Empty<int>();
            movie.UsersThatRatedMovie = Array.Empty<string>();

            _movieService.Create(movie);

            return RedirectToAction("Index","Movies");
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(Movie movie, string id, IFormFile image)
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
                //TODO this should not be here
                movie.ImagePath = updateMovie.ImagePath;
                movie.Rating = updateMovie.Rating;
                movie.UsersThatRatedMovie = updateMovie.UsersThatRatedMovie;
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


        [HttpPost]        
        [ValidateAntiForgeryToken]
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


        [HttpPost]
        public ActionResult Rate(string id)
        {
            var getMovie = _movieService.Get(id);

            var user = _userManager.GetUserAsync(User);

            string userId = user.Result.Id.ToString();

            if (!getMovie.UsersThatRatedMovie.Contains(userId))
            {
                if (getMovie == null)
                {
                    return NotFound();
                }

                var userThatRated = getMovie.UsersThatRatedMovie.Append(userId);

                var usersRating = getMovie.Rating.Append(Int32.Parse(Request.Form["rating"]));

                getMovie.Rating = usersRating.ToArray();

                getMovie.UsersThatRatedMovie = userThatRated.ToArray();

                _movieService.Update(id, getMovie);

                return RedirectToAction("Details", "Movies", new { id });
            }
            
            int index = Array.IndexOf(getMovie.UsersThatRatedMovie, userId);

            getMovie.Rating[index] = Int32.Parse(Request.Form["rating"]);

            _movieService.Update(id, getMovie);

            return RedirectToAction("Details", "Movies", new { id });


        }
    }
}
