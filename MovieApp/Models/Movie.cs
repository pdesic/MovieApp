using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieApp.Models
{
    public class Movie
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; }
        public string Name { get; set; }
        public string ReleaseDate { get; set; }
        public Genre Genre { get; set; }
        public string MovieTrailerUrl { get; set; }
        public string ImagePath { get; set; }
    }
}
