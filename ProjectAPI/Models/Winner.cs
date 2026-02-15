namespace ProjectAPI.Models
{
    public class Winner
    {
        public int Id { get; set; }

        public int GiftId { get; set; }
        public Gifts Gift { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
    }

}
