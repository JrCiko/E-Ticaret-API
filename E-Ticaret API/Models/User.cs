using System.ComponentModel.DataAnnotations;

namespace E_Ticaret_API.Models
{
    public class User
    {
        [Key] // Bu attribute, Id'nin birincil anahtar olduğunu belirtir
        public int Id { get; set; }

        [Required] // Bu alanın boş bırakılamayacağını belirtir
        [StringLength(50)] // Maksimum karakter uzunluğunu belirler
        public string Username { get; set; } // Genellikle e-posta adresi de kullanılabilir

        [Required]
        public string PasswordHash { get; set; }
    }
}
