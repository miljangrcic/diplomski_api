using Diplomski.Utilities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Diplomski.DTOs.Product
{
    public class ProductUpdateDTO
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [MaxLength(13)]
        public string EANCode { get; set; }

        [ValidFileExtensions(allowedFileExtensions: ".jpg, .png")]
        public IFormFile ProductImage { get; set; }

        public string Description { get; set; }

        [Required]
        [Column(TypeName = "DECIMAL(7,2)")]
        public decimal Price { get; set; }
        [Required]
        public int CategoryID { get; set; }
        [Required]
        public int ManufacturerID { get; set; }
        [Required]
        public int VolumeID { get; set; }
        [Required]
        public int PackagingMaterialID { get; set; }
    }
}
