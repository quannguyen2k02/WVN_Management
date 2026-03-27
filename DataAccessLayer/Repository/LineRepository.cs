using DataAccessLayer.Data;
using DataAccessLayer.IRepository;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Repository
{
    public class LineRepository : ILineRepository
    {
        private readonly ApplicationDbContext _context;
        public LineRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<int?> GetIdByLineName(string name)
        {
            var lineId = await _context.Lines
        .Where(l => l.Name.ToLower() == name.ToLower())
        .Select(l =>(int?) l.Id)
        .FirstOrDefaultAsync();
            return lineId;
        }


        public async Task<string?> GetLineNameById(int id)
        {
            var lineName = await _context.Lines
                .Where(l => l.Id == id)
                .Select(l => (string?)l.Name).FirstOrDefaultAsync();
            return lineName;
        }
    }
}
