using BusinessLogicLayer.IServices;
using DataAccessLayer.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ATD_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LedModelController : ControllerBase
    {
        public ILedModelService _LedModelService { get; set; }
        public LedModelController(ILedModelService ledModelService)
        {
            _LedModelService = ledModelService;
        }
        [HttpPost]
        public async Task<IActionResult> AddLedModelAsync(LedModel ledmodel)
        {
            var result = await _LedModelService.AddLedModelAsync(ledmodel);
            return Ok(result);
        }
        [HttpGet]
        public async Task<IActionResult> GetCurrentLedModelAsync(string line, string devicename, string model, string kb, string fp)
        {
            var result = await _LedModelService.GetLedModelAsync(line, devicename, model, kb, fp);
            return Ok(result);
        }
    }
}
