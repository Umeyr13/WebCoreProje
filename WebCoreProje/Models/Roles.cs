using System.ComponentModel.DataAnnotations;

namespace WebCoreProje.Models
{
    public class Roles
    {
        public Roles()
        {
            this.User = new List<User>();
        }
        [StringLength(20)]
        public string Id { get; set; }

        [StringLength(20)]
        public string Name { get; set; }

        public virtual ICollection<User> User { get; set; }
    }
}
