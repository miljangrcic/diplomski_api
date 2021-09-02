using Diplomski.Data.Interfaces;
using Diplomski.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Diplomski.Data.Repositories
{
    public class SQLProductRepository : IProductRepository
    {
        private readonly ApplicationContext _context;

        public SQLProductRepository(ApplicationContext context)
        {
            _context = context;
        }
        public IEnumerable<Product> GetProducts(
            string nameFilter, 
            string[] categoryFilters, 
            string[] manufacturerFilters, 
            string[] packageMaterialFilters, 
            decimal[] volumeFilters
        )
        {
            var query = this._context.Products.AsQueryable();

            if(!String.IsNullOrEmpty(nameFilter))
            {
                query = query.Where(product => product.Name.ToLower().Contains(nameFilter.ToLower()));
            }

            if(categoryFilters.Length > 0)
            {
                query = query.Where(product => categoryFilters.Contains(product.Category.Name));
            }

            if(manufacturerFilters.Length > 0)
            {
                query = query.Include(table => table.Manufacturer).Where(product => manufacturerFilters.Contains(product.Manufacturer.Name));
            }

            if(packageMaterialFilters.Length > 0)
            {
                query = query.Include(table => table.PackagingMaterial).Where(product => packageMaterialFilters.Contains(product.PackagingMaterial.Name));
            }

            if(volumeFilters.Length > 0)
            {
                query = query.Include(table => table.Volume).Where(product => volumeFilters.Contains(product.Volume.Amount));
            }

            return query.AsEnumerable();
        }

        public Product GetProductByID(int id)
        {
            return _context.Products
                .Include(table => table.Category)
                .Include(table => table.Manufacturer)
                .Include(table => table.Volume)
                .Include(table => table.PackagingMaterial)
                .SingleOrDefault(product => product.ProductID.Equals(id));
        }

        public void CreateProduct(Product product)
        {
            _context.Products.Add(product);
        }

        public void UpdateProduct(Product product)
        {
            // no need for implementation
        }

        public void DeleteProduct(Product product)
        {
            _context.Products.Remove(product);
        }

        public IEnumerable<Volume> GetAllVolumes()
        {
            return _context.Volumes;
        }

        public IEnumerable<PackagingMaterial> GetAllPackagingMaterials()
        {
            return _context.PackagingMaterials;
        }

        public IEnumerable<Category> GetAllCategories()
        {
            return _context.Categories;
        }

        public bool SaveChanges()
        {
            return _context.SaveChanges() > 0;
        }
    }
}
