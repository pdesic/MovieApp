using AspNetCore.Identity.MongoDbCore.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieApp.Models
{
    public class User:MongoIdentityUser<ObjectId>
    {
        //[BsonId]
        //[BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        //public string Id { get; set; }
        //public string FirstName { get; set; }
        //public string LastName { get; set; }
        //public string Username { get; set; }
        //public string Password { get; set; }
        //public string Email { get; set; }


        public bool isAdmin { get; set; }
        public User() : base()
        {
        }

        public User(string userName, string email) : base(userName, email)
        {
        }
    }
}
