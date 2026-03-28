using BusinessLogicLayer.ModelDTOs.LED;
using DataAccessLayer.Common;
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
        public Task<PagedResult<dynamic>> GetLedModelAsync(string line, string devicename, string model, string kb, string fp, int pageNumber = 1, int pageSize = 20);
        public Task<PagedResult<dynamic>> GetLedModelsByDevice(string line, string devicename, int pageNumber = 1, int pageSize = 20);
        public Task<dynamic> GetLedModelById(int id);
    }
}
