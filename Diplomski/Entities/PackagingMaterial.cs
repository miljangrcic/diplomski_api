using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Diplomski.Entities
{
    public class PackagingMaterial
    {
        public int PackagingMaterialID { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        public ICollection<Product> Products { get; set; }
    }
}