using Diplomski.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Diplomski.Data.Interfaces
{
    public interface IProductRepository
    {
        public IEnumerable<Product> GetProducts(
            string nameFilter, 
            string[] categoryFilters,
            string[] manufacturerFilters, 
            string[] packageMaterialFilters,
            decimal[] volumeFilters
        );
        public Product GetProductByID(int id);
        public void CreateProduct(Product product);
        public void UpdateProduct(Product product);
        public void DeleteProduct(Product product);
        public IEnumerable<Volume> GetAllVolumes();
        public IEnumerable<PackagingMaterial> GetAllPackagingMaterials();
        public IEnumerable<Category> GetAllCategories();
        public bool SaveChanges();

    }
}
