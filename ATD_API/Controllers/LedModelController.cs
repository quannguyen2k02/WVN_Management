using BusinessLogicLayer.IServices;
using BusinessLogicLayer.ModelDTOs.LED;
using Microsoft.AspNetCore.Mvc;
using OpenQA.Selenium;

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
        public async Task<IActionResult> AddLedModelAsync(LedModelDTO ledmodelDTO)
        {
            try
            {
                var result = await _LedModelService.AddLedModelAsync(ledmodelDTO);
                return Ok(result);

            }
            catch (NotFoundException ex)
            {
                return NotFound(new { Message = ex.Message });
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "Internal server error" });
            }
        }
        [HttpGet("by-model-version")]
        public async Task<IActionResult> GetLedModelAsync([FromQuery] string line, [FromQuery] string devicename, [FromQuery] string model, [FromQuery] string kb, [FromQuery] string fp, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 20)
        {
            try
            {
                var result = await _LedModelService.GetLedModelAsync(line, devicename, model, kb, fp, pageNumber, pageSize);
                return Ok(result);
            }
            catch (NotFoundException ex)
            {
                return NotFound(new { Message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "Internal server error" });
            }
            
        }
        [HttpGet("by-device")]
        public async Task<IActionResult> GetLedModelByDevice([FromQuery] string line, [FromQuery] string deviceName, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 20)
        {
            try
            {
                if (string.IsNullOrEmpty(line) || string.IsNullOrEmpty(deviceName))
                {
                    return BadRequest(new { Message = "Line and DeviceName are required." });
                }
                var result = await _LedModelService.GetLedModelsByDevice(line, deviceName, pageNumber, pageSize);
                return Ok(result);
            }
            catch (NotFoundException ex)
            {
                return NotFound(new { Message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = ex.Message });
            }
        }
        [HttpGet("by-id")]
        public async Task<IActionResult> GetLedModelById(int id)
        {
            try
            {
                var result = await _LedModelService.GetLedModelById(id);
                return Ok(result);
            }
            catch(NotFoundException ex)
            {
                return NotFound(new { Message = ex.Message });
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "Internal server error" });
            }
        }

    }
}
