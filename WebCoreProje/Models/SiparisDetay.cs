using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebCoreProje.Models
{
    public class SiparisDetay
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public Nullable<int> Adet { get; set; }
        public Nullable<int> ToplamTutar { get; set; }

        public virtual Siparis Siparis { get; set; }
        public virtual Urunler? Urunler { get; set; }
    }
}
