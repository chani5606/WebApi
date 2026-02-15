using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ProjectAPI.Models
{
    public class User
    {
       public int Id { get; set; }
       public string FirstName { get; set; }
       public string LastName { get; set; }
        public string Password { get; set; }
       public string Phone { get;set; }
       public string Email { get; set; }
        public string City { get; set; }

        public string Nieghbrhood { get; set; }

        public string Street { get; set; }

        public string Role { get; set; } = "User";

        public List<Basket> Gifts { get; set; } = new List<Basket>();

    }
}
