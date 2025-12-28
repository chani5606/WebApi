using System.ComponentModel.DataAnnotations;

namespace ProjectFinal.Dto
{
    public class DonorCreateDto
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        [Phone]
        [MaxLength(20)]
        public string Phon { get; set; }
        [Required]
        [EmailAddress]
        [MaxLength(200)]
        public string Email { get; set; }
    }

    public class DonorUpdateDto
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        [Phone]
        [MaxLength(20)]
        public string Phon { get; set; }
        [Required]
        [EmailAddress]
        [MaxLength(200)]
        public string Email { get; set; }
    }


    public class DonorResponseDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Phon { get; set; }
        public string Email { get; set; }
    }
}

