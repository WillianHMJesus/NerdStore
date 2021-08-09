using AutoMapper;
using NerdStore.Catalog.Application.DTOs;
using NerdStore.Catalog.Domain.Entities;

namespace NerdStore.Catalog.Application.AutoMapper
{
    public class DomainToDTOMappingProfile : Profile
    {
        public DomainToDTOMappingProfile()
        {
            CreateMap<Product, ProductDTO>()
                .ForMember(e => e.Height, d => d.MapFrom(x => x.Dimension.Height))
                .ForMember(e => e.Width, d => d.MapFrom(x => x.Dimension.Width))
                .ForMember(e => e.Depth, d => d.MapFrom(x => x.Dimension.Depth));

            CreateMap<Category, CategoryDTO>();
        }
    }
}
