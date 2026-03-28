using DataAccessLayer.Common;
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

        public async Task<PagedResult<LedModel>> GetLedModelAsync(int deviceId, string model, string kb, string fp, int pageNumber = 1, int pageSize = 20)
        {
            // Sử dụng .Include() để nạp các bảng liên quan, tránh bị null dữ liệu
            var query = _context.LEDModels
                .Include(x => x.LEDModelConfigs)
                .Include(x => x.Cameras)
                    .ThenInclude(c => c.LedStatuses)
                        .ThenInclude(c => c.Jobs)
                .Where(x => x.KB == kb && x.FP == fp && x.LedId == deviceId && x.Name.ToLower() == model.ToLower());

            var totalCount = await query.CountAsync();

            var items = await query
                .OrderBy(x => x.Id)
                .Skip((Math.Max(pageNumber, 1) - 1) * Math.Max(pageSize, 1))
                .Take(Math.Max(pageSize, 1))
                .ToListAsync();

            return new PagedResult<LedModel>
            {
                Items = items,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalCount = totalCount
            };
        }

        public async Task<PagedResult<LedModel>> GetLedModelsByDeviceIdAsync(int id, int pageNumber = 1, int pageSize = 20)
        {
            // Đảm bảo pageNumber và pageSize hợp lệ
            pageNumber = Math.Max(pageNumber, 1);
            pageSize = Math.Max(pageSize, 1);

            // Bước 1: Lấy danh sách Id lớn nhất cho mỗi nhóm (Name, KB, FP)
            var latestIdsQuery = _context.LEDModels
                .Where(x => x.LedId == id)
                .GroupBy(x => new { x.Name, x.KB, x.FP })
                .Select(g => g.Max(x => x.Id)); // Lấy Id lớn nhất trong mỗi nhóm

            // Đếm tổng số nhóm (chính là số lượng bản ghi sẽ trả về sau khi lọc)
            var totalCount = await latestIdsQuery.CountAsync();

            // Phân trang trên các Id (cần sắp xếp để phân trang nhất quán)
            var pagedIds = await latestIdsQuery
                .OrderBy(x => x) // hoặc theo tiêu chí khác
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            // Bước 2: Lấy chi tiết các bản ghi dựa trên danh sách Id
            var items = await _context.LEDModels
                .Where(x => pagedIds.Contains(x.Id))
                .OrderBy(x => x.Id) // sắp xếp theo thứ tự Id để phù hợp với phân trang
                .ToListAsync();

            return new PagedResult<LedModel>
            {
                Items = items,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalCount = totalCount
            };
        }

        public async Task<LedModel> GetLedModelById(int id)
        {
            var ledModel = await _context.LEDModels
                .FindAsync(id);
            return ledModel;
        }
    }
}
