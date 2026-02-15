using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectAPI.Models
{
    public class Basket
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
        public int GiftsId { get; set; }

        public Gifts Gifts { get; set; }
        public int Status { get; set; } = 0;



    }
}
