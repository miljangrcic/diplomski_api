using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Diplomski.Entities
{
    public class Product
    {
        public int ProductID { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [MaxLength(13)]
        public string EANCode { get; set; }

        [Required]
        [MaxLength(255)]
        public string ImageFileName { get; set; }

        public string Description { get; set; }

        [Required]
        [Column(TypeName="DECIMAL(7,2)")]
        public decimal Price { get; set; }
        public int CategoryID { get; set; }
        public Category Category { get; set; }
        public int ManufacturerID { get; set; }
        public Manufacturer Manufacturer { get; set; }
        public int VolumeID { get; set; }
        public Volume Volume  { get; set; }
        public int PackagingMaterialID { get; set; }
        public PackagingMaterial PackagingMaterial { get; set; }


    }
}
