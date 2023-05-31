using System.ComponentModel.DataAnnotations;

namespace WebCoreProje.Models.ViewModel
{
    public class EditUserModel
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
        [StringLength(20), Required]
        public string Role { get; set; }

        public bool Aktivate { get; set; }
    }
}
