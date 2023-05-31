using System.ComponentModel.DataAnnotations;

namespace WebCoreProje.Models.ViewModel
{
    public class UserModel
    {   //Burada sadece lazım olan propertileri alıyoruz. Yönetici Şifreyi görmeyecek.Kat bilgileri vs gibi istemediğimiz şeyler ViewModel Sayesinde gitmiş oluyor
        
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public bool Aktivate { get; set; }
        public DateTime CreateDate { get; set; }
        public string Role { get; set; }
        public string ProfilImagefileName { get; set; }
    }
}
