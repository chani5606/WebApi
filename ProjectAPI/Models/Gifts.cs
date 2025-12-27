using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectAPI.Models
{
    public class Gifts
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int GiftNumber { get; set; }
        public int  IdDonor { get; set; }
        public Donors Donor { get; set; }
        public int  IdCatgory { get; set; }
        public Catgories Catgory { get; set; }
        public int Price { get; set; }
        public string PathImage { get; set; }
    }
}
