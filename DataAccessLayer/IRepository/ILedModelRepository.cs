using DataAccessLayer.Common;
using DataAccessLayer.Entities.LED;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.IRepository
{
    public interface ILedModelRepository
    {
        public Task<PagedResult<LedModel>> GetLedModelAsync(int deviceId, string model, string kb, string fp, int pageNumber = 1, int pageSize = 20);
        public Task<LedModel> AddLedModelAsync(LedModel ledModel);
        public Task<LedModelConfig> AddLedModelConfig(LedModelConfig ledModelConfig);
        public Task<PagedResult<LedModel>> GetLedModelsByDeviceIdAsync(int id, int pageNumber = 1, int pageSize = 20);
        public Task<LedModel> GetLedModelById(int id);

    }
}