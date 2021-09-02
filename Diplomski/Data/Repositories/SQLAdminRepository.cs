using Diplomski.Data.Interfaces;
using Diplomski.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Diplomski.Data.Repositories
{
    public class SQLAdminRepository : IAdminRepository
    {
        private readonly ApplicationContext _context;
        public SQLAdminRepository(ApplicationContext context)
        {
            _context = context;
        }
        public Admin GetAdminByUsernameAndPassword(string username, string password)
        {
            return _context.Admins
                    .SingleOrDefault(admin => 
                        admin.Username == username 
                        && admin.Password == password
                     );
        }
    }
}
