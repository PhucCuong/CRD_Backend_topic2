using AutoMapper;
using API_Sample.Data.Entities;
using API_Sample.Models.Common;
using API_Sample.Models.Response;

namespace API_Sample.Application.Mapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            //Common
            CreateMap<Image, MRes_Image>();
            CreateMap<Image, BaseModel.Image>();

            //Main
            
            CreateMap<Product, MRes_Product>();
            CreateMap<Post, MRes_Post>();
            CreateMap<Account, MRes_Account>();
        }
    }
}
