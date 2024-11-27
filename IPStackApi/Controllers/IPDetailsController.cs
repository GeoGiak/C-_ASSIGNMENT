
using IPStackDLL.Models;

using Microsoft.AspNetCore.Mvc;
using IPStackApi.Services;

using IPStackApi.Dtos;
using IPStackApi.Models;

namespace IPStackApi.Controllers {

    [Route("api/[controller]")]
    [ApiController]
    public class IPDetailsController : ControllerBase
    {
        private readonly IPDetailsService _ipDetailsService;
        private readonly UpdateBatchJobService _updateBatchJobService;

        public IPDetailsController(IPDetailsService ipDetailsService, UpdateBatchJobService updateBatchJobService) 
        {
            _ipDetailsService = ipDetailsService;
            _updateBatchJobService = updateBatchJobService;
        }

        [HttpGet("{ipAddress}")]
        public async Task<ActionResult<IPDetails>> GetIPDetails(string ipAddress)
        {
            try 
            {
                var details = await _ipDetailsService.getDetails(ipAddress);
                return Ok(details);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"{ex.Message}");
            }
            
        }

        [HttpPut("batch-update")]
        public async Task<ActionResult<Guid>> CreateTask([FromBody] List<IPDetailsRequestDto> ipDetailsList)
        {
            Console.WriteLine("Im in");
            
            Guid jobId = _updateBatchJobService.StartBatchUpdateAsync(ipDetailsList).Result;
            return Ok(jobId);
        }

        [HttpGet("job-progress/{jobId}")]
        public ActionResult JobProgres(Guid jobId)
        {
            try 
            {
                BatchUpdateJob job = _updateBatchJobService.GetJobProgress(jobId);
                return Ok("Ready: " + job.ProcessedItems + "/" + job.TotalItems);
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

    }
}
