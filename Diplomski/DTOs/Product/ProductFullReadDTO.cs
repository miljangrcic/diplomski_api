using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Diplomski.DTOs.Product
{
    public class ProductFullReadDTO
    {
        public int ProductID { get; set; }
        public string Name { get; set; }
        public string EANCode { get; set; }
        public string ImageURL { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int CategoryID { get; set; }
        public string Category { get; set; }
        public int ManufacturerID { get; set; }
        public string Manufacturer { get; set; }
        public int VolumeID { get; set; }
        public string Volume { get; set; }
        public int PackagingMaterialID { get; set; }
        public string PackagingMaterial { get; set; }
    }
}

