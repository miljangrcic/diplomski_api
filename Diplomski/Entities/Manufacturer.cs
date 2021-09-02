using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Diplomski.Entities
{
    public class Manufacturer
    {
        public int ManufacturerID { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        public string Description { get; set; }

        [Required]
        [MaxLength(255)]
        public string LogoImageFileName { get; set; }

        [Required]
        [MaxLength(255)]
        public string BannerImageFileName { get; set; }

        [Required]
        [MaxLength(20)]
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

        public ICollection<Product> Products { get; set; }
    }
}