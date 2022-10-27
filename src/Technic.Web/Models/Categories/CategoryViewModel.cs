namespace Technic.Web.Models.Categories
{
    public class CategoryViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }
        public byte[]? CategoryImage { get; set; }
    }
}
