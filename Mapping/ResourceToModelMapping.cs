using AutoMapper;
using Spartan.Models;
using Spartan.Resources;

namespace Spartan.Mapping
{
    public class ResourceToModelProfile : Profile
    {
        public ResourceToModelProfile()
        {
            CreateMap<SaveCategoryResource, Category>();
        }
    }
}
