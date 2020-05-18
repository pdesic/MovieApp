using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieApp.Models
{
    public class MovieAppDBSettings : IMovieAppDBSettings
    {
        public string MoviesCollectionName { get; set; }
        public string UsersCollectionName { get; set; }
        public string GenresCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }

    public interface IMovieAppDBSettings
    {
        public string MoviesCollectionName { get; set; }
        public string UsersCollectionName { get; set; }
        public string GenresCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
