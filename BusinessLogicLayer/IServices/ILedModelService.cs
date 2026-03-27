using BusinessLogicLayer.ModelDTOs.LED;
using DataAccessLayer.Entities.LED;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.IServices
{
    public interface ILedModelService
    {
        public Task<LedModel> AddLedModelAsync(LedModelDTO ledModelDTO);
        public Task<List<LedModelDTO>> GetLedModelAsync(string line, string devicename, string model, string kb, string fp);
        public Task<List<LedModelDTO>> GetLedModelsByDevice(string line, string devicename);
        public Task<LedModelDTO> GetLedModelById(int id);
    }
}
