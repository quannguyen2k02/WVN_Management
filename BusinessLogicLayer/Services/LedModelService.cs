using BusinessLogicLayer.IServices;
using DataAccessLayer.Entities;
using DataAccessLayer.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services
{
    public class LedModelService : ILedModelService
    {
        public readonly ILedModelRepository _LedModelRepository;
        public LedModelService(ILedModelRepository ledModelRepository)
        {
            _LedModelRepository = ledModelRepository;
        }
        public async Task<LedModel> AddLedModelAsync(LedModel ledModel)
        {
            return await _LedModelRepository.AddLedModelAsync(ledModel);
        }

        public async Task<LedModel> GetLedModelAsync(string line, string devicename, string model, string kb, string fp)
        {
            return await _LedModelRepository.GetLedModelAsync(line, devicename, model, kb, fp);
        }
    }
}
