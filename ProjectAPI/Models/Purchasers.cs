using System.ComponentModel.DataAnnotations;

namespace ProjectAPI.Models
{
    public class Purchasers
    {
       public int Id { get; set; }
       public string FisrtName { get; set; }
       public string LastName { get; set; }
       public string Phone { get;set; }
       public string Email { get; set; }
       public Adress Adress { get; set; }

        //public List<Basket> Gifts { get; set; } = new List<Basket>();

    }
}
