using System.Linq;
using AutoMapper;
using Spartan.Extensions;
using Spartan.Models;
using Spartan.Resources;

namespace Spartan.Common
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<User, UserDetailDto>()
                .ForMember(dest => dest.PhotoUrl, opt =>
                {
                    opt.MapFrom(src => src.Photos.FirstOrDefault(p => p.IsMain).Url);
                })
                .ForMember(dest=>dest.Age, opt=> {
                    opt.ResolveUsing(src=>src.DateOfBirth.CalculateAge());
                });
            CreateMap<Photo, PhotoDto>();
        }
    }
}