using AutoMapper;
using BusinessLogicLayer.IServices;
using BusinessLogicLayer.ModelDTOs.LED;
using DataAccessLayer.Entities.LED;
using DataAccessLayer.IRepository;
using OpenQA.Selenium;

namespace BusinessLogicLayer.Services
{
    public class LedModelService : ILedModelService
    {
        private readonly ILedModelRepository _LedModelRepository;
        private readonly ILedRepository _LedRepository;
        private readonly ILineRepository _LineRepository;
        private readonly IMapper _mapper;
        public LedModelService(ILedModelRepository ledModelRepository, IMapper mapper,ILedRepository ledRepository,ILineRepository lineRepository )
        {
            _LedModelRepository = ledModelRepository;
            _mapper = mapper;
            _LedRepository = ledRepository;
            _LineRepository = lineRepository;
        }
        public async Task<LedModel> AddLedModelAsync(LedModelDTO ledModelDTO)
        {
            int? lineId = await _LineRepository.GetIdByLineName( ledModelDTO.LineName);
            if (!lineId.HasValue)
                throw new NotFoundException($"Line with name '{ledModelDTO.LineName}' was not found.");

            int? deviceId = await _LedRepository.GetDeviceIdByDeviceNameAndLineNameAsync(ledModelDTO.DeviceName, (int)lineId);
            if (!deviceId.HasValue)
            {
                throw new NotFoundException($"Device with name '{ledModelDTO.DeviceName}' was not found.");
            }
            ledModelDTO.LedId = (int)deviceId;
            var ledModel = _mapper.Map<LedModel>(ledModelDTO);
            return await _LedModelRepository.AddLedModelAsync(ledModel);
        }

        public async Task<List<LedModelDTO>> GetLedModelAsync(string line, string devicename, string model, string kb, string fp)
        {
            int? lineId = await _LineRepository.GetIdByLineName(line);
            if(!lineId.HasValue)
                throw new NotFoundException($"Line with name '{line}' was not found.");
            
            int? deviceId = await _LedRepository.GetDeviceIdByDeviceNameAndLineNameAsync(devicename, (int)lineId);
            if (!deviceId.HasValue)
                throw new NotFoundException($"Device with name '{devicename}' in line '{line}' was not found.");

            return _mapper.Map<List<LedModelDTO>>( await _LedModelRepository.GetLedModelAsync((int)deviceId, model, kb, fp));
        }

        public async Task<List<LedModelDTO>> GetLedModelsByDevice(string line, string devicename)
        {
            int? lineId = await _LineRepository.GetIdByLineName(line);
            if (!lineId.HasValue)
                throw new NotFoundException($"Line with name '{line}' was not found.");

            int? deviceId = await _LedRepository.GetDeviceIdByDeviceNameAndLineNameAsync(devicename, (int)lineId);
            if (!deviceId.HasValue)
                throw new NotFoundException($"Device with name '{devicename}' in line '{line}' was not found.");
            return _mapper.Map<List<LedModelDTO>>(await _LedModelRepository.GetLedModelsByDeviceIdAsync((int)deviceId));
        }

        public async Task<LedModelDTO> GetLedModelById(int id)
        {
            var model = await _LedModelRepository.GetLedModelById(id);
            if(model == null) 
                throw new NotFoundException($"Model with ID {id} was not found!"); 
            return _mapper.Map<LedModelDTO>(model);
        }
    }
}
