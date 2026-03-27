using DataAccessLayer.Data;
using DataAccessLayer.Entities;
using DataAccessLayer.IRepository;
using Microsoft.EntityFrameworkCore;
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

        public async Task<int?> GetDeviceIdByDeviceNameAndLineNameAsync(string deviceName, int lineId)
        {
            return await _context.LEDs.Where(x => x.Name == deviceName && x.LineId == lineId).Select(x=> (int?)x.Id).FirstOrDefaultAsync();
        }

        public async Task<string?> GetDeviceNameByIdAsync(int id)
        {
            return await _context.LEDs.Where(x=>x.Id == id).Select(x=>x.Name).FirstOrDefaultAsync();

        }
    }
}
