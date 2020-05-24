using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MovieApp.Models
{
    public class Movie
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; }
        [Required]
        [Display(Name = "Movie name")]

        public string Name { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Release date")]
        public string ReleaseDate { get; set; }
        //[Required]
        public string[] Genre { get; set; }
        [Display(Name = "Youtube url trailer")]
        [Required]
        [Url]
        public string MovieTrailerUrl { get; set; }
        [Display(Name = "Import movie image")]
        public string ImagePath { get; set; }
    }
}