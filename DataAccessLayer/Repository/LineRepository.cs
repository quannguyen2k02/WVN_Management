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
    public class LineRepository : ILineRepository
    {
        private readonly ApplicationDbContext _context;
        public LineRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<int> GetIdByLineName(string name)
        {
            var lineId = await _context.Lines
        .Where(l => l.Name.Equals(name, StringComparison.OrdinalIgnoreCase))
        .Select(l => l.Id)
        .FirstOrDefaultAsync();
            return lineId;
        }


        public async Task<string> GetLineNameById(int id)
        {
            var lineName = await _context.Lines
                .Where(l => l.Id == id)
                .Select(l => l.Name).FirstOrDefaultAsync();
            return lineName;
        }
    }
}
