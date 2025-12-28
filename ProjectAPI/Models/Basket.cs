using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectAPI.Models
{
    public class Basket
    {
        public int Id { get; set; }

        public int PurchasersId { get; set; }
        //public Purchasers Purchaser { get; set; }
        public int GiftsId { get; set; }

        public Gifts Gifts { get; set; }



    }
}
