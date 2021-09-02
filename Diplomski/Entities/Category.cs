using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Diplomski.Entities
{
    public class Category
    {
        public int CategoryID { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        public int? ParentCategoryID { get; set; }

        public Category ParentCategory { get; set; }

        public ICollection<Product> Products { get; set; }
    }
}