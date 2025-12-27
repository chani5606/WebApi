using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectAPI.Models
{
    public class Basket
    {
        public int Id { get; set; }

        public int IdPurchaser { get; set; }
        [ForeignKey("IdPurchaser")]
        public Purchasers Purchaser { get; set; }
        public int IdGifts { get; set; }

        [ForeignKey("IdGifts")]
        public Gifts Gifts { get; set; }



    }
}
