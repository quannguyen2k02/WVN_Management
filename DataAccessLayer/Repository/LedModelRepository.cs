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
    public class LedModelRepository : ILedModelRepository
    {
        private readonly ApplicationDbContext _context;
        public LedModelRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Task<LedModelConfig> AddLedModelConfig(LedModelConfig ledModelConfig)
        {
            throw new NotImplementedException();
        }

         async Task<LedModel> ILedModelRepository.AddLedModelAsync(LedModel ledModel)
        {
            await _context.LEDModels.AddAsync(ledModel);
            await _context.SaveChangesAsync();
            return ledModel;
        }

        async Task<LedModel> ILedModelRepository.GetLedModelAsync(string line, string devicename, string model, string kb, string fb)
        {
            // Sử dụng .Include() để nạp các bảng liên quan, tránh bị null dữ liệu
            var result = await _context.LEDModels
                .Include(x => x.LEDModelConfigs)
                .Include(x => x.Cameras)
                    .ThenInclude(c => c.LedStatuses)
                        .ThenInclude(c => c.Jobs)
                .Where(x => x.KB == kb && x.FP == fb)
                .FirstOrDefaultAsync();

            return result;
        }
    }
}
