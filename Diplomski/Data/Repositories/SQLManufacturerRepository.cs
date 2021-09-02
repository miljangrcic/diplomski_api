using Diplomski.Data.Interfaces;
using Diplomski.Entities;
using System.Collections.Generic;

namespace Diplomski.Data.Repositories
{
    public class SQLManufacturerRepository : IManufacturerRepository
    {
        private readonly ApplicationContext _context;


        public SQLManufacturerRepository(ApplicationContext context)
        {
            _context = context;
        }


        public IEnumerable<Manufacturer> GetAllManufacturers()
        {
            return _context.Manufacturers;
        }

        public Manufacturer GetManufacturerByID(int id)
        {
            return _context.Manufacturers.Find(id);
        }

        public void CreateManufacturer(Manufacturer manufacturer)
        {
            _context.Manufacturers.Add(manufacturer);
        }

        public void UpdateManufacturer(Manufacturer manufacturer)
        {
            // no need for implementation
        }

        public void DeleteManufacturer(Manufacturer manufacturer) {
            _context.Manufacturers.Remove(manufacturer);
        }

        public bool SaveChanges()
        {
            return _context.SaveChanges() >= 1;
        }





    }
}
