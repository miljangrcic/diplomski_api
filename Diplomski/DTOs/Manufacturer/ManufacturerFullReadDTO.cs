using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Diplomski.DTOs.Manufacturer
{
    public class ManufacturerFullReadDTO
    {
        public int ManufacturerID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string LogoURL { get; set; }
        public string BannerURL { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Fax { get; set; }
        public string Website { get; set; }

    }
}
