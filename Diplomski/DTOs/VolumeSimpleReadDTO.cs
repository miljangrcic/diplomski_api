using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Diplomski.DTOs
{
    public class VolumeSimpleReadDTO
    {
        public int VolumeID { get; set; }
        public decimal Amount { get; set; }
        public string MeasuringUnitAbbreviation { get; set; }

    }
}
