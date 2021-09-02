using AutoMapper;
using Diplomski.DTOs.Manufacturer;
using Diplomski.Entities;
using Diplomski.Profiles.Resolvers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Options;

namespace Diplomski.Profiles
{
    public class ManufacturerProfile : Profile
    {
        public ManufacturerProfile()
        {
            CreateMap<Manufacturer, ManufacturerSimpleReadDTO>()
                .ForMember(dest => dest.LogoURL, options => options.MapFrom<ManufacturersLogoPathResolver>());

            CreateMap<Manufacturer, ManufacturerFullReadDTO>()
                .ForMember(dest => dest.LogoURL, options => options.MapFrom<ManufacturersLogoPathResolver>())
                .ForMember(dest => dest.BannerURL, options => options.MapFrom<ManufacturersBannerPathResolver>());

            CreateMap<ManufacturerCreateDTO, Manufacturer>()
                .ForMember(dest => dest.BannerImageFileName, options => options.Ignore())
                .ForMember(dest => dest.LogoImageFileName, options => options.Ignore());

            CreateMap<ManufacturerUpdateDTO, Manufacturer>()
                .ForMember(dest => dest.BannerImageFileName, options => options.Ignore())
                .ForMember(dest => dest.LogoImageFileName, options => options.Ignore());
        }




    }
}
