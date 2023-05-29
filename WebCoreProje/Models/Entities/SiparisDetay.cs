using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebCoreProje.Models.Entities
{
    public class SiparisDetay
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int? Adet { get; set; }
        public int? ToplamTutar { get; set; }

        public virtual Siparis Siparis { get; set; }
        public virtual Urunler? Urunler { get; set; }
    }
}
