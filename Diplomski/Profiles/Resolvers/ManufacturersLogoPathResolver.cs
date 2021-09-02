using AutoMapper;
using Diplomski.DTOs.Manufacturer;
using Diplomski.Entities;
using Microsoft.Extensions.Options;

namespace Diplomski.Profiles.Resolvers
{
    public class ManufacturersLogoPathResolver : IValueResolver<Manufacturer, object, string>
    {
        private readonly IOptions<ImagesPathsOptions> _imagesPaths;

        public ManufacturersLogoPathResolver(IOptions<ImagesPathsOptions> imagesPaths)
        {
            _imagesPaths = imagesPaths;
        }
        public string Resolve(Manufacturer source, object destination, string destMember, ResolutionContext context)
        {
            return _imagesPaths.Value.BaseURL + _imagesPaths.Value.ManufacturerLogoPath + source.LogoImageFileName;
        }
    }
}
