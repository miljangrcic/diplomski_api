using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Diplomski.Entities
{
    public class Volume
    {
        public int VolumeID { get; set; }

        [Required]
        [Column(TypeName="DECIMAL(4,2")]
        public decimal Amount { get; set; }

        public string MeasuringUnitAbbreviation { get; set; }

        public ICollection<Product> Products { get; set; }
    }
}