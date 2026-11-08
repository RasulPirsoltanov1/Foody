using AutoMapper;
using Foody.WebApi.Dtos.FeatureDtos;
using Foody.WebApi.Dtos.MessageDtos;
using Foody.WebApi.Dtos.ProductDtos;
using Foody.WebApi.Dtos.TestimonialDtos;
using Foody.WebApi.Entites;

namespace Foody.WebApi.Mapping
{
    public class GeneralMapping : Profile
    {
        public GeneralMapping()
        {
            CreateMap<Feature, ResultFeatureDto>().ReverseMap();
            CreateMap<Feature, CreateFeatureDto>().ReverseMap();
            CreateMap<Feature, UpdateFeatureDto>().ReverseMap();
            CreateMap<Feature, GetByIdFeatureDto>().ReverseMap();

            CreateMap<Message, ResultMessageDto>().ReverseMap();
            CreateMap<Message, CreateMessageDto>().ReverseMap();
            CreateMap<Message, UpdateMessageDto>().ReverseMap();
            CreateMap<Message, GetByIdMessageDto>().ReverseMap();

            CreateMap<Product, CreateProductDto>().ReverseMap();
            CreateMap<Product, ResultProductWithCategoryDto>().ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.CategoryName)).ReverseMap();

            CreateMap<Testimonial, ResultTestimonialDto>().ReverseMap();
         
        }
    }
}
