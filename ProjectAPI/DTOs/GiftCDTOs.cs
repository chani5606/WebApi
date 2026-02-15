using System.ComponentModel.DataAnnotations;

namespace ProjectAPI.Dto
{
    public class GiftCreateDTOs
    {
           [Required]
            [MaxLength(50) ]
            public string Name { get; set; }
           [Required]
            public int GiftNumber { get; set; }

            public int DonorId { get; set; }      
            public int CatgoryId { get; set; }
           [Required]
           [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0")]
           public int Price { get; set; }
           public string PathImage { get; set; }
    }
    public class GiftUpdateDTOs
    {
        [MaxLength(50)]
        public string Name { get; set; }
        public int GiftNumber { get; set; }
        public int DonorId { get; set; }
        public int CatgoryId { get; set; }
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0")]
        public int Price { get; set; }
        public string PathImage { get; set; }
    }
    public class GifttResponseDTOs

    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int GiftNumber { get; set; }
        public int DonorId { get; set; }
        public string nameDonors { get; set; }
        public int CatgoryId { get; set; }
        public string CategoryName { get; set; }
        public int Price { get; set; }
        public string PathImage { get; set; }
    }


}
