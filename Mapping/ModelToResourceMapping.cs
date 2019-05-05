using AutoMapper;
using Spartan.Models;
using Spartan.Resources;

namespace Spartan.Mapping
{
    public class ModelToResourceProfile : Profile
    {
        public ModelToResourceProfile()
        {
            CreateMap<Category, CategoryResource>();
        }
    }
}
