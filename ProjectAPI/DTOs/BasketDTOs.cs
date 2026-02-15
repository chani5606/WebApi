using ProjectAPI.Models;
using System.ComponentModel.DataAnnotations;

namespace ProjectAPI.DTOs
{
   public class BasketCreateDTO
    {
        [Required]
        public int UserId { get; set; }
        [Required]

        public int GiftsId { get; set; }
    }
    public class BasketUpdateDTO {
        [Required]

        public int UserId { get; set; }
        [Required]

        public int GiftsId { get; set; }
        public int Status { get; set; }
    }
    public class BasketResponseDTO {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string User { get; set; }
        public int GiftsId { get; set; }

        public int Status { get; set; }
      
            public string GiftName { get; set; }
            public string CategoryName { get; set; }
            public string DonorName { get; set; }
            public decimal Price { get; set; }
        public string pathImage
        {
            get; set;
        }

    }
}
