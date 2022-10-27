using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Technic.Web.Data.Entites
{
    public class User : IdentityUser
    {
        [Key]
        public Guid Id { get; set; }
        [MaxLength(100)]
        public string? Name { get; set; }
        [MaxLength(100)]
        public string? Surname { get; set; }
        [MaxLength(100)]
        public string? Patronymic { get; set; }
        [MaxLength(100)]
        public byte[]? Profile { get; set; }
        public List<Order> Order { get; set; }
        public DateTime RegisteredAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime LastLogin { get; set; }
    }
}
