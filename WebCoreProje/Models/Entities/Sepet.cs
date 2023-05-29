using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebCoreProje.Models.Entities
{
    public class Sepet
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int? Adet { get; set; }
        public int? ToplamTutar { get; set; }

        public virtual User? User { get; set; }
        public virtual Urunler? Urunler { get; set; }

    }
}
