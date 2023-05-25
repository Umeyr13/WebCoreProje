﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebCoreProje.Models
{
    [Table("User")]
    public class User
    {
        [Key]
        public Guid Id { get; set; }

        [StringLength(30)]
        public string? Name { get; set; }

        [StringLength(30)]
        public string? Surname { get; set; }

        [Required,StringLength(50)]
        public string Email { get; set; }

        [Required,StringLength(30)]
        public string Username { get; set; }

        [Required,StringLength(250)]
        public string Pasword { get; set; }

        public bool Aktivate { get; set; }
        public DateTime CreateDate { get; set; }

    }
}
