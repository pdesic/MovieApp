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
        public Movie Movie { get; set; }
        public Genre Genre { get; set; }

    }
}