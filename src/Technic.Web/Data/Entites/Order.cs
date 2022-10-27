using System.ComponentModel.DataAnnotations.Schema;

namespace Technic.Web.Data.Entites
{
    public class Order
    {
        public Guid Id { get; set; }
        public Product Product { get; set; }
        [ForeignKey("Product")]
        public Guid ProductId { get; set; }
        public User User { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }
        public string Address { get; set; }
        public string PostIndex { get; set; }
    }
}
