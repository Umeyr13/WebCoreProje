using System.ComponentModel.DataAnnotations;

namespace WebCoreProje.Models
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "Kullanıcı adı boş geçilemez"), StringLength(30)]
        public string Username { get; set; }


        [Required(ErrorMessage = "şifre boş geçilemez"), MinLength(6), MaxLength(16)]
        public string Pasword { get; set; }

        [Required(ErrorMessage = "şifre boş geçilemez"), MinLength(6)
        , MaxLength(16),Compare(nameof(Pasword))]
        public string Pasword2 { get; set; }

        [Required(ErrorMessage = "Email boş geçilemez"), StringLength(50)]
        public string Email { get; set; }
    }
}
