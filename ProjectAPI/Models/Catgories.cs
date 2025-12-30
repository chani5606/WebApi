namespace ProjectAPI.Models
{
    public class Catgories
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Gifts> Gifts { get; set; } = new List<Gifts>();
    }
}
