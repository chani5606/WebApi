using System.ComponentModel.DataAnnotations;

namespace ProjectAPI.DTOs
{
   
      public  class UserCreateDTO
        {
          
            [Required]
            [MaxLength(100)]
            public string FirstName { get; set; }

            [Required]
            [MaxLength(100)]
            public string LastName { get; set; }


            [Required]
            [MaxLength(50)]
            public string Phone { get; set; }
            [Required]
            [MaxLength(50)]
            [EmailAddress]
            public string Email { get; set; }
            [Required]
            [MaxLength(50)]
            [MinLength(8, ErrorMessage = "Password must be at least 8 characters")]
            public string Password { get; set; }
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
      public  class UserUpdateDTO
        {
           
            [Required]
            [MaxLength(100)]
            public string FirstName { get; set; }
            [Required]
            [MaxLength(100)]
            public string LastName { get; set; }
            [Required]
            [MaxLength(50)]
            public string Phone { get; set; }
            [Required]
            [MaxLength(50)]
            [EmailAddress]
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
        public class UserResponseDTO
        {
             public int Id { get; set; }
           public string FirstName { get; set; }

            public string LastName { get; set; }

            public string Phone { get; set; }
            public string Email { get; set; }
            public string City { get; set; }
            public string Nieghbrhood { get; set; }
            public string Street { get; set; }
        }

    }

