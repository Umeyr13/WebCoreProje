using System.ComponentModel.DataAnnotations;

namespace WebCoreProje.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage ="Kullanıcı adı boş geçilemez"),StringLength(30)]
        public string Username  { get; set; }


        [Required(ErrorMessage ="şifre boş geçilemez"),MinLength(6),MaxLength(16)]
        public string Pasword { get; set; }
    }
}
