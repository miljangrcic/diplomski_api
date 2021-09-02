using AutoMapper;
using Diplomski.DTOs;
using Diplomski.DTOs.Product;
using Diplomski.Entities;
using Diplomski.Profiles.Resolvers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Diplomski.Profiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductSimpleReadDTO>()
                .ForMember(dest => dest.ImageURL, options => options.MapFrom<ProductLogoPathResolver>());

            CreateMap<Product, ProductFullReadDTO>()
                .ForMember(dest => dest.ImageURL, options => options.MapFrom<ProductLogoPathResolver>())
                .ForMember(dest => dest.Category, options => options.MapFrom(source => source.Category.Name))
                .ForMember(dest => dest.Manufacturer, options => options.MapFrom(source => source.Manufacturer.Name))
                .ForMember(dest => dest.Volume, options => options.MapFrom(source => source.Volume.Amount + source.Volume.MeasuringUnitAbbreviation))
                .ForMember(dest => dest.PackagingMaterial, options => options.MapFrom(source => source.PackagingMaterial.Name));

            CreateMap<ProductCreateDTO, Product>();

            CreateMap<ProductUpdateDTO, Product>();

            CreateMap<Volume, VolumeSimpleReadDTO>();

            CreateMap<PackagingMaterial, PackagingMaterialSimpleReadDTO>();

            CreateMap<Category, CategorySimpleReadDTO>();
        }
    }
}
