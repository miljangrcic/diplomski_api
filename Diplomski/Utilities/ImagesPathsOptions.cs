using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Diplomski
{
    // Paths to different image folders inside wwwroot, binded from appsettings.json
    public class ImagesPathsOptions
    {
        public const string ImagesPaths = "ImagesPaths";

        public string BaseURL { get; set; }
        public string ManufacturerLogoPath { get; set; }
        public string ManufacturerBannerPath { get; set; }
        public string ProductImagePath { get; set; }
    }
}
