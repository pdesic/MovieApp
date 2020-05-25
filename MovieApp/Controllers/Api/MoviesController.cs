using MovieApp.Models;
using MovieApp.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.AspNetCore.Cors;
using System;
using System.Net.Http;
using System.Net;

namespace MovieApp.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : Controller
    {
        private readonly MovieService _movieService;

        public MoviesController(MovieService movieService)
        {
            _movieService = movieService;
        }

        [HttpGet]
        public ActionResult<List<Movie>> Get() =>
            _movieService.Get();

        // [HttpGet("{id:length(24)}", Name = "GetMovie")]
        // public ActionResult<Movie> Get(string id)
        // {
        //     var movie = _movieService.Get(id);
        //
        //     if (movie == null)
        //     {
        //         return NotFound();
        //     }
        //
        //     return movie;
        // }
        //
        // [HttpPost]
        // public ActionResult<Movie> Create(Movie movie)
        // {
        //     _movieService.Create(movie);
        //
        //     return CreatedAtRoute("GetMovie", new { id = movie.Id.ToString() }, movie);
        // }
        //
        // [HttpPut("{id:length(24)}")]
        // public IActionResult Update(string id, Movie movieIn)
        // {
        //     var movie = _movieService.Get(id);
        //
        //     if (movie == null)
        //     {
        //         return NotFound();
        //     }
        //
        //     _movieService.Update(id, movieIn);
        //
        //     return NoContent();
        // }
        //
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