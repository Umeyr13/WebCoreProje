using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebCoreProje.Models.Entities
{
    [Table("Kategoriler")]
    public class Kategoriler
    {

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [DisplayName("Kategori"), StringLength(50)]
        public string? KategoriAdi { get; set; }

        public virtual ICollection<Urunler>? Urunler { get; set; }

        public Kategoriler()
        {
            Urunler = new List<Urunler>();
        }
    }
}
