﻿using Microsoft.EntityFrameworkCore;
using WebCoreProje.Models;
using WebCoreProje.Models.Entities;

namespace WebCoreProje.Models
{
    public class DatabaseContext:DbContext
    {
        public DatabaseContext(DbContextOptions options) : base(options)
        {
        }

        public virtual DbSet<User> Users { get; set; }

        public DbSet<WebCoreProje.Models.Urunler>? Urunler { get; set; }
    }
}
