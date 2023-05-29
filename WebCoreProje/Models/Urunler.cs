﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Microsoft.AspNetCore.DataProtection;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebCoreProje.Models
{
    public class Urunler
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [DisplayName("Urun Adı"), Required]
        [StringLength(50)]
        public string UrunAdi { get; set; }

        [DisplayName("Urun Açıklaması")]
        [StringLength(200)]
        public string UrunAciklamasi { get; set; }

        [DisplayName("Urun Fiyatı")]
        public Nullable<int> UrunFiyati { get; set; }

        public virtual Kategoriler Kategoriler { get; set; }

        public virtual ICollection<Sepet> Sepet { get; set; }

        public virtual ICollection<SiparisDetay> SiparisDetay { get; set; }

        public Urunler()
        {
            this.Sepet = new List<Sepet>();
            this.SiparisDetay = new List<SiparisDetay>();
        }
    }
}
