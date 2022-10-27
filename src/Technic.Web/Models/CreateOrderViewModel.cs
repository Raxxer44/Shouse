using System.ComponentModel.DataAnnotations;
using Technic.Web.Data.Entites;

namespace Technic.Web.Models
{
    public class CreateOrderViewModel
    {
        [Required]
        public Guid ProductId { get; set; }
        [Required]
        public Guid UserId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        [Required]
        public string Patronymic { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string Index { get; set; }
    }
}
