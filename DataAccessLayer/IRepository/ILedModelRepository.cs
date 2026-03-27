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
        public Task<List<LedModel>> GetLedModelAsync(int deviceId, string model, string kb, string fp);
        public Task<LedModel> AddLedModelAsync(LedModel ledModel);
        public Task<LedModelConfig> AddLedModelConfig(LedModelConfig ledModelConfig);
        public Task<List<LedModel>> GetLedModelsByDeviceIdAsync(int id);
        public Task<LedModel> GetLedModelById(int id);
        
    }
}
