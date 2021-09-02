using AutoMapper;
using Diplomski.Entities;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Diplomski.Profiles.Resolvers
{
    public class ProductLogoPathResolver : IValueResolver<Product, object, string>
    {
        ImagesPathsOptions _options;
        public ProductLogoPathResolver(IOptions<ImagesPathsOptions> options)
        {
            _options = options.Value;
        }

        public string Resolve(Product source, object destination, string destMember, ResolutionContext context)
        {
            return $"{_options.BaseURL}{_options.ProductImagePath}{source.ImageFileName}";
        }
    }
}
