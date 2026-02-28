using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.IServices
{
    public interface ILedModelService
    {
        public Task<LedModel> AddLedModelAsync(LedModel ledModel);
        public Task<LedModel> GetLedModelAsync(string line, string devicename, string model, string kb, string fp);
    }
}
