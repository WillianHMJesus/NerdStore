using AutoMapper;
using NerdStore.Catalog.Application.ViewModels;
using NerdStore.Catalog.Domain.Entities;
using NerdStore.Catalog.Domain.ValueObjects;

namespace NerdStore.Catalog.Application.AutoMapper
{
    public class ViewModelToEntityMappingProfile : Profile
    {
        public ViewModelToEntityMappingProfile()
        {
            CreateMap<ProductViewModel, Product>()
                .ConstructUsing(x => new Product(x.Name, x.Description, x.Value.Value, x.Image, x.Quantity.Value,
                                     new Dimension(x.Height.Value, x.Width.Value, x.Depth)));

            CreateMap<CategoryViewModel, Category>()
                .ConstructUsing(x => new Category(x.Code.Value, x.Name, x.Description));
        }
    }
}
