namespace ProjectAPI.DTOs
{
    public class WinnerCreatedDTO
    {
        public int GiftId { get; set; }
        public int UserId { get; set; }

    }
    public class WinnerResponseDTO
    {
        public int Id { get; set; }
        public int GiftId { get; set; }
        public string Gift { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
    }
}
