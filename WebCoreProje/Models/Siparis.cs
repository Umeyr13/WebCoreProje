using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebCoreProje.Models
{
    [Table("Siparis")]
    public class Siparis
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        
        [StringLength(50)]
        public string? Ad { get; set; }
        [StringLength(80)]
        public string? Soyad { get; set; }
        [StringLength(200)]
        public string? Adres { get; set; }
        [StringLength(20)]
        public string? Telefon { get; set; }
        [StringLength(11)]
        public string? TcKimlikNo { get; set; }
        public Nullable<System.DateTime> Tarih { get; set; }

        public virtual User User { get; set; }

        public virtual ICollection<SiparisDetay> SiparisDetay { get; set; }

        public Siparis()
        {
            this.SiparisDetay = new List<SiparisDetay>();
        }
    }
}
