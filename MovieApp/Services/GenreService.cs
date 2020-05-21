using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MovieApp.Models;
using MongoDB.Driver;

namespace MovieApp.Services
{
    public class GenreService
    {
        private readonly IMongoCollection<Genre> _genres;

        public GenreService(IMovieAppDBSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _genres = database.GetCollection<Genre>(settings.GenresCollectionName);
        }

        public List<Genre> Get() =>
            _genres.Find(genre => true).ToList();
    }
}
