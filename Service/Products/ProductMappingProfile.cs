using App.Repositories.Products;
using App.Service.Products.Create;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Service.Products
{
    public class ProductMappingProfile : Profile
    {
        public ProductMappingProfile()
        {
            CreateMap<Product, ProductDto>().ReverseMap();
            // Profile ile map edilecek türlerbelirlenir  
            // Servis katmanında gerekli konfigurasyonlar yapılır.
            // Bussiness da automapper IMapper interface'i aracılığıyla kullanılabilir.
            CreateMap<CreateProductRequest, Product>().ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name.ToLowerInvariant()));
        }
    }
}
