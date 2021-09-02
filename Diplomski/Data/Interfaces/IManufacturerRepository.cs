using Diplomski.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Diplomski.Data.Interfaces
{
    public interface IManufacturerRepository
    {
        IEnumerable<Manufacturer> GetAllManufacturers();
        Manufacturer GetManufacturerByID(int manufacturerID);
        void CreateManufacturer(Manufacturer m);
        void UpdateManufacturer(Manufacturer m);
        void DeleteManufacturer(Manufacturer m);
        bool SaveChanges();

    }
}
