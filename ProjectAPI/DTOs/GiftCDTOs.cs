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
            public int IdDonor { get; set; }      
            public int IdCatgory { get; set; }
           [Required]
           [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0")
           public int Price { get; set; }
           public string PathImage { get; set; }
    }
    public class GiftUpdateDTOs
    {
        [MaxLength(50)]
        public string Name { get; set; }
        public int GiftNumber { get; set; }
        public int IdDonor { get; set; }
        public int IdCatgory { get; set; }
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0")
        public int Price { get; set; }
        public string PathImage { get; set; }
    }
    public class GifttResponseDTOs
    {
        public string Name { get; set; }
        public int GiftNumber { get; set; }
        public int IdDonor { get; set; }
        public string nameDonors { get; set; }
        public int IdCatgory { get; set; }
        public string CategoryName { get; set; }
        public int Price { get; set; }
        public string PathImage { get; set; }
    }


}
