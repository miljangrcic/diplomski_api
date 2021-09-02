using Diplomski.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Diplomski.Data.Interfaces
{
    public interface IAdminRepository
    {
        Admin GetAdminByUsernameAndPassword(string username, string password);
    }
}
