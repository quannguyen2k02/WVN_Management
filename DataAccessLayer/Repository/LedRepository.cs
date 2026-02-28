using DataAccessLayer.Data;
using DataAccessLayer.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repository
{
    public class LedRepository : ILedRepository
    {
        private readonly ApplicationDbContext _context;
        public LedRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public Task<string> GetDeviceNameByIdAsync(int id)
        {
            throw new NotImplementedException();

        }

        public Task<int> GetIdDeviceByLineNameAsync(string lineName)
        {
            throw new NotImplementedException();
        }
    }
}
