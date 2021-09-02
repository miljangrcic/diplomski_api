using AutoMapper;
using Diplomski.Entities;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Diplomski.Profiles.Resolvers
{
    public class ManufacturersBannerPathResolver : IValueResolver<Manufacturer, object, string>
    {
        private readonly IOptions<ImagesPathsOptions> _imagesPaths;

        public ManufacturersBannerPathResolver(IOptions<ImagesPathsOptions> imagesPaths)
        {
            _imagesPaths = imagesPaths;
        }
        public string Resolve(Manufacturer source, object destination, string destMember, ResolutionContext context)
        {
            return _imagesPaths.Value.BaseURL + _imagesPaths.Value.ManufacturerBannerPath + source.BannerImageFileName;
        }
    }
}
