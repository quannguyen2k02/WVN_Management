using AutoMapper;
using BusinessLogicLayer.ExternalServices.Mapper;
using BusinessLogicLayer.IServices;
using BusinessLogicLayer.ModelDTOs.LED;
using DataAccessLayer.Common;
using DataAccessLayer.Entities.LED;
using DataAccessLayer.IRepository;
using OpenQA.Selenium;
using System.Dynamic;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services
{
    public class LedModelService : ILedModelService
    {
        private readonly ILedModelRepository _LedModelRepository;
        private readonly ILedRepository _LedRepository;
        private readonly ILineRepository _LineRepository;
        private readonly IMapper _mapper;
        public LedModelService(ILedModelRepository ledModelRepository, IMapper mapper, ILedRepository ledRepository, ILineRepository lineRepository)
        {
            _LedModelRepository = ledModelRepository;
            _mapper = mapper;
            _LedRepository = ledRepository;
            _LineRepository = lineRepository;
        }
        /// <summary>
        /// AddNewLedModel
        /// </summary>
        /// <param name="ledModelDTO"></param>
        /// <returns></returns>
        /// <exception cref="NotFoundException"></exception>
        public async Task<LedModelDTO> AddLedModelAsync(LedModelDTO ledModelDTO)
        {
            int? lineId = await _LineRepository.GetIdByLineName(ledModelDTO.LineName);
            if (!lineId.HasValue)
                throw new NotFoundException($"Line with name '{ledModelDTO.LineName}' was not found.");

            int? deviceId = await _LedRepository.GetDeviceIdByDeviceNameAndLineNameAsync(ledModelDTO.DeviceName, (int)lineId);
            if (!deviceId.HasValue)
            {
                throw new NotFoundException($"Device with name '{ledModelDTO.DeviceName}' was not found.");
            }
            ledModelDTO.LedId = (int)deviceId;
            var ledModel = _mapper.Map<LedModel>(ledModelDTO);
            return _mapper.Map<LedModelDTO>(await _LedModelRepository.AddLedModelAsync(ledModel));
        }

        /// <summary>
        /// Get LedMode by line, device name, model name, kb and fp. This will return all version that match the param
        /// </summary>
        /// <param name="line"></param>
        /// <param name="devicename"></param>
        /// <param name="model"></param>
        /// <param name="kb"></param>
        /// <param name="fp"></param>
        /// <returns></returns>
        /// <exception cref="NotFoundException"></exception>
        public async Task<PagedResult<dynamic>> GetLedModelAsync(string line, string devicename, string model, string kb, string fp, int pageNumber = 1, int pageSize = 20)
        {
            int? lineId = await _LineRepository.GetIdByLineName(line);
            if (!lineId.HasValue)
                throw new NotFoundException($"Line with name '{line}' was not found.");

            int? deviceId = await _LedRepository.GetDeviceIdByDeviceNameAndLineNameAsync(devicename, (int)lineId);
            if (!deviceId.HasValue)
                throw new NotFoundException($"Device with name '{devicename}' in line '{line}' was not found.");

            var repoPage = await _LedModelRepository.GetLedModelAsync((int)deviceId, model, kb, fp, pageNumber, pageSize);
            var mapped = _mapper.Map<List<LedModelDTO>>(repoPage.Items);
            var resultList = new List<dynamic>();
            foreach (var item in mapped)
            {
                resultList.Add(LED_MapToDynamic.MapToDynamic(item));
            }

            return new PagedResult<dynamic>
            {
                Items = resultList,
                PageNumber = repoPage.PageNumber,
                PageSize = repoPage.PageSize,
                TotalCount = repoPage.TotalCount
            };
        }
        /// <summary>
        /// get led model by line, device name. This will return all the latest of each model that match the line and device name
        /// </summary>
        /// <param name="line"></param>
        /// <param name="devicename"></param>
        /// <returns></returns>
        /// <exception cref="NotFoundException"></exception>
        public async Task<PagedResult<dynamic>> GetLedModelsByDevice(string line, string devicename, int pageNumber = 1, int pageSize = 20)
        {
            int? lineId = await _LineRepository.GetIdByLineName(line);
            if (!lineId.HasValue)
                throw new NotFoundException($"Line with name '{line}' was not found.");

            int? deviceId = await _LedRepository.GetDeviceIdByDeviceNameAndLineNameAsync(devicename, (int)lineId);
            if (!deviceId.HasValue)
                throw new NotFoundException($"Device with name '{devicename}' in line '{line}' was not found.");
            var repoPage = await _LedModelRepository.GetLedModelsByDeviceIdAsync((int)deviceId, pageNumber, pageSize);
            var mapped = _mapper.Map<List<LedModelDTO>>(repoPage.Items);
            var resultList = new List<dynamic>();
            foreach (var item in mapped)
            {
                resultList.Add(LED_MapToDynamic.MapToDynamic(item));
            }
            return new PagedResult<dynamic>
            {
                Items = resultList,
                PageNumber = repoPage.PageNumber,
                PageSize = repoPage.PageSize,
                TotalCount = repoPage.TotalCount
            };
        }
        /// <summary>
        /// get led model by id. This will return the model that match the id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="NotFoundException"></exception>
        public async Task<dynamic> GetLedModelById(int id)
        {
            var model = await _LedModelRepository.GetLedModelById(id);
            if (model == null)
                throw new NotFoundException($"Model with ID {id} was not found!");
            var mapped = _mapper.Map<LedModelDTO>(model);
            return LED_MapToDynamic.MapToDynamic(mapped);
        }

    }
}
