using MovieApp.Models;
using MovieApp.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.AspNetCore.Cors;
using System;
using System.Net.Http;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace MovieApp.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : Controller
    {
        private readonly MovieService _movieService;
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;


        public MoviesController(MovieService movieService, SignInManager<User> signInManager, UserManager<User> userManager)
        {
            _movieService = movieService;
            _signInManager = signInManager;
            _userManager = userManager;

        }

        [HttpGet]
        public Object Get()
        {
            var user = _userManager.GetUserAsync(User);
            
            
            if (_signInManager.IsSignedIn(User))
            {
                return new
                {
                    movies = _movieService.Get(),
                    isAdmin = user.Result.isAdmin
                };
            }
            
            return new
            {
                movies = _movieService.Get(),
                isAdmin = false
            };
        }


        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            try
            {
                var movie = _movieService.Get(id);

                if (movie == null)
                {
                    return NotFound();
                }

                _movieService.Remove(movie.Id);

                return Json(new
                {
                    Message = "Movie deleted successfully",
                    Code = 200
                });
            }

            catch(Exception ex)
            {
                return Json(new
                {
                    Message = ex.Message,
                    Code = 400
                });
            }
        }
    }
}