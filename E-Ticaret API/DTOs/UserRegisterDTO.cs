using System.ComponentModel.DataAnnotations;

namespace E_Ticaret_API.DTOs
{
    public class UserRegisterDto
    {
        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string Username { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 6)] // Şifreye minimum uzunluk koymak iyidir
        public string Password { get; set; }
    }
}
