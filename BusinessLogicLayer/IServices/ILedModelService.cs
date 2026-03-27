using BusinessLogicLayer.ModelDTOs.LED;
using DataAccessLayer.Entities.LED;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.IServices
{
    public interface ILedModelService
    {
        public Task<LedModelDTO> AddLedModelAsync(LedModelDTO ledModelDTO);
        public Task<List<dynamic>> GetLedModelAsync(string line, string devicename, string model, string kb, string fp);
        public Task<List<dynamic>> GetLedModelsByDevice(string line, string devicename);
        public Task<dynamic> GetLedModelById(int id);
    }
}
