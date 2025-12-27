using System.ComponentModel.DataAnnotations;

namespace ProjectAPI.Models
{
    public class Purchasers
    {
       public int Id { get; set; }
        [Required]
       public string FisrtName { get; set; }
        [Required]
       public string LastName { get; set; }
        [Required]
       public string Phone { get;set; }
       public string Email { get; set; }
       [Required]
       public Adress Adress { get; set; }


    }
}
