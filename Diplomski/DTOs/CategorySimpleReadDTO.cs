using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Diplomski.DTOs
{
    public class CategorySimpleReadDTO
    {
        public int CategoryID { get; set; }
        public string Name { get; set; }
        public int ParentCategoryID { get; set; }
    }
}
