using System;
namespace ProductsApp.Models
{
	public class User
    {

        public string Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public User(string id, string username, string password) => (Id, Username, Password) = (id, username, password);
    }
}


