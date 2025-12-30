using System.ComponentModel.DataAnnotations;

namespace ProjectFinal.Dto
{
    public class DonorCreateDTOs
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        [Phone]
        [MaxLength(20)]
        public string Phone { get; set; }
        [Required]
        [EmailAddress]
        [MaxLength(200)]
        public string Email { get; set; }
        [Required]
        [MaxLength(50)]
        public string City { get; set; }
        [Required]
        [MaxLength(50)]
        public string Nieghbrhood { get; set; }

        [Required]
        [MaxLength(50)]
        public string Street { get; set; }


    }

    public class DonorUpdateDTOs
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        [Phone]
        [MaxLength(20)]
        public string Phone { get; set; }
        [Required]
        [EmailAddress]
        [MaxLength(200)]
        public string Email { get; set; }
        [Required]
        [MaxLength(50)]
        public string City { get; set; }
        [Required]
        [MaxLength(50)]
        public string Nieghbrhood { get; set; }

        [Required]
        [MaxLength(50)]
        public string Street { get; set; }
    }


    public class DonorResponseDTOs
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }

        public string City { get; set; }
        public string Nieghbrhood { get; set; }
        public string Street { get; set; }

    }
}

