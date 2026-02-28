using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.IRepository
{
    public interface ILedModelRepository
    {
        public Task<LedModel> GetLedModelAsync(string line, string devicename, string model, string kb, string fb);
        public Task<LedModel> AddLedModelAsync(LedModel ledModel);
        public Task<LedModelConfig> AddLedModelConfig(LedModelConfig ledModelConfig);
        
    }
}
