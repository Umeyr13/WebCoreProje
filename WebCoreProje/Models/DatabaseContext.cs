using Microsoft.EntityFrameworkCore;
using WebCoreProje.Models.Entities;
using WebCoreProje.Models.ViewModel;

namespace WebCoreProje.Models
{
    public class DatabaseContext:DbContext
    {
        public DatabaseContext(DbContextOptions options) : base(options)
        {
        }

        public virtual DbSet<User> Users { get; set; }

        public DbSet<Urunler>? Urunler { get; set; }

        public DbSet<Kategoriler> Kategoriler { get; set; }

        public DbSet<WebCoreProje.Models.ViewModel.UserModel>? UserModel { get; set; }

        public DbSet<WebCoreProje.Models.ViewModel.CreateUserModel>? CreateUserModel { get; set; }

        public DbSet<WebCoreProje.Models.ViewModel.EditUserModel>? EditUserModel { get; set; }
    }
}
