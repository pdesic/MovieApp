using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MovieApp.Models;


namespace MovieApp.ViewModels
{
    public class MovieFormViewModel
    {
        // public IEnumerable<Genre> Genres { get; set; }
        //
        // [BsonId]
        // [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        // public string Id { get; set; }
        // public string Name { get; set; }
        // public string ReleaseDate { get; set; }
        // public Genre Genre { get; set; }
        // public string MovieTrailerUrl { get; set; }
        // public string ImagePath { get; set; }
        //
        // public MovieFormViewModel()
        // {
        //     ObjectId id = ObjectId.GenerateNewId();
        //       
        //     Id = id.ToString();
        // }
        //
        // public MovieFormViewModel(Movie movie)
        // {
        //     Id = movie.Id;
        //     Name = movie.Name;
        //     ReleaseDate = movie.ReleaseDate;
        //     Genre = movie.Genre;
        //     MovieTrailerUrl = movie.MovieTrailerUrl;
        //     ImagePath = movie.ImagePath;
        // }
    }
}
