using DataAccessLayer.Data;
using DataAccessLayer.Entities.LED;
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

         public async Task<LedModel> AddLedModelAsync(LedModel ledModel)
        {
            ledModel.CreateDate = DateTime.Now;
            ledModel.ModifiedDate = DateTime.Now;
            await _context.LEDModels.AddAsync(ledModel);
            await _context.SaveChangesAsync();
            return ledModel;
        }

        public async Task<List<LedModel>> GetLedModelAsync(int deviceId, string model, string kb, string fp)
        {
            // Sử dụng .Include() để nạp các bảng liên quan, tránh bị null dữ liệu
            var result = await _context.LEDModels
                .Include(x => x.LEDModelConfigs)
                .Include(x => x.Cameras)
                    .ThenInclude(c => c.LedStatuses)
                        .ThenInclude(c => c.Jobs)
                .Where(x => x.KB == kb && x.FP == fp && x.LedId == deviceId && x.Name.ToLower() == model.ToLower())
                .OrderBy(x=>x.Id)
                .ToListAsync();

            return result;
        }

        public async Task<List<LedModel>> GetLedModelsByDeviceIdAsync(int id)
        {
            var list = await _context.LEDModels
                .Where(x => x.LedId == id)
                // Nhóm các bản ghi trùng Name, Kb, Fp lại với nhau
                .GroupBy(x => new { x.Name, x.KB, x.FP })
                .Select(g => g.OrderByDescending(x => x.Id) // Sắp xếp theo ID giảm dần trong mỗi nhóm
                              .FirstOrDefault())            // Lấy thằng đầu tiên (thằng mới nhất)
                .ToListAsync();

            return list;
        }

        public async Task<LedModel> GetLedModelById(int id)
        {
            var ledModel = await _context.LEDModels
                .FindAsync(id);
            return ledModel;
        }
    }
}
