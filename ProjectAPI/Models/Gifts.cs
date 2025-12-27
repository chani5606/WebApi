using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectAPI.Models
{
    public class Gifts
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int GiftNumber { get; set; }
        public int  IdDonor { get; set; }
        [ForeignKey("IdDonor")]
        public Donors Donor { get; set; }
        public int  IdCatgory { get; set; }
        [ForeignKey("IdCatgory")]
        public Catgories Catgory { get; set; }

        public int Price { get; set; }
        public string PathImage { get; set; }
    }
}
