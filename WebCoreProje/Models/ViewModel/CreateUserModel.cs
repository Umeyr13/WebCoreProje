using System.ComponentModel.DataAnnotations;

namespace WebCoreProje.Models.ViewModel
{
    public class CreateUserModel
    {
        [Key]
        public Guid Id { get; set; }

        [StringLength(30)]
        public string? Name { get; set; }

        [StringLength(30)]
        public string? Surname { get; set; }

        [Required, StringLength(50)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Kullanıcı adı boş geçilemez"), StringLength(30)]
        public string Username { get; set; }


        [Required(ErrorMessage = "şifre boş geçilemez"), MinLength(6), MaxLength(16)]
        public string Pasword { get; set; }

        [Required(ErrorMessage = "şifre2 boş geçilemez"), MinLength(6), MaxLength(16), Compare(nameof(Pasword))]
        public string Pasword2 { get; set; }

        [StringLength(20), Required]
        public string Role { get; set; }

        public bool Aktivate { get; set; }
    }
}
