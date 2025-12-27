using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectAPI.Models
{
    public class Basket
    {
        public int Id { get; set; }

        public int IdPurchaser { get; set; }
        public Purchasers Purchaser { get; set; }
        public int IdGifts { get; set; }

        public Gifts Gifts { get; set; }



    }
}
