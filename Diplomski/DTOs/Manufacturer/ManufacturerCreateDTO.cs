using Diplomski.Utilities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Diplomski.DTOs.Manufacturer
{
    public class ManufacturerCreateDTO
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        public string Description { get; set; }

        [Required]
        [ValidFileExtensions(allowedFileExtensions: ".jpg, .png")]
        public IFormFile LogoImageFile { get; set; }

        [Required]
        [ValidFileExtensions(allowedFileExtensions: ".jpg, .png")]
        public IFormFile BannerImageFile { get; set; }

        [Required]
        [MaxLength(50)]
        public string Phone { get; set; }

        [Required]
        [MaxLength(50)]
        public string Address { get; set; }

        [Required]
        [MaxLength(50)]
        public string Email { get; set; }

        [MaxLength(50)]
        public string Fax { get; set; }

        [MaxLength(50)]
        public string Website { get; set; }
    }
}
