using System.Text.Json.Serialization;

namespace ProjectAPI.Models
{
    public class Donors
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Phone { get; set; }
        public string Email { get; set; }
        public List<Gifts> Gifts { get; set; }  = new List<Gifts>();
         
        public string City { get; set; }

        public string Nieghbrhood { get; set; }

        public string Street { get; set; }


    }
}
