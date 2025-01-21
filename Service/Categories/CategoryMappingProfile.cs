using App.Repositories.Categories;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Service.Categories
{
    public class CategoryMappingProfile : Profile
    {
        public CategoryMappingProfile() 
        {
            CreateMap<Category, CategoryDto>().ReverseMap();
            // CreateMap<CreateCategoryRequest, Category>(); ve ayrıca Requestdeki name parametresi küçük harfle category nesnesine map edilsin
            CreateMap<CreateCategoryRequest, Category>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name.ToLower()));

            CreateMap<Category , CategoryWithProductsDto>().ReverseMap();

            //CreateMap<UpdateCategoryRequest, Category>()
            //.ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name.ToLower()));
        }
    }
}
