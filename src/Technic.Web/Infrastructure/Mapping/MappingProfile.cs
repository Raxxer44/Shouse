using AutoMapper;
using Technic.Web.Data.Entites;
using Technic.Web.Models.Categories;
using Technic.Web.Models.Product;

namespace Technic.Web.Infrastructure.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ProductCategory, CategoryViewModel>().ReverseMap();
            CreateMap<CreateCategoryViewModel, ProductCategory>().ReverseMap();
            CreateMap<Product, CreateProductViewModel>().ReverseMap();

            CreateMap<Product, ShortProductViewModel>().ReverseMap();
            CreateMap<Product, ProductViewModel>().ReverseMap();
            CreateMap<CreateCategoryViewModel, ProductViewModel>().ReverseMap();
        }
    }
}
