using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using PEPRN231_SU24_009909_Repo.Models;
using PEPRN231_SU24_009909_Service;

namespace PEPRN231_SU24_009909_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FootballPlayerController : Controller
    {
        private readonly FootballPlayerService _service;

        public FootballPlayerController(FootballPlayerService service)
        {
            _service = service;
        }
        [Authorize(Roles = "1,2")]
        [HttpGet("get")]
        public async Task<IActionResult> Get()
        {
            var result = await _service.Get();
            return Ok(result);
        }
        [Authorize(Roles = "1,2")]
        [HttpGet("getById")]
        public async Task<IActionResult> GetById(string id)
        {
            var result = await _service.GetById(id);
            return Ok(result);
        }

        [Authorize(Roles = "1,2")]
        [HttpGet("search")]
        [EnableQuery] 
        public async Task<IActionResult> Search()
        {
            var result = await _service.Search();
            return Ok(result);
        }
        [Authorize(Roles = "1")]
        [HttpPost("add")]
        public async Task<IActionResult> Add(FootballPlayer request)
        {
            var result = await _service.Add(request);
            return Ok(result);
        }
        [Authorize(Roles = "1")]
        [HttpPut("update")]
        public async Task<IActionResult> Update(string id, FootballPlayer request)
        {
            var result = await _service.Update(id, request);
            return Ok(result);
        }
        [Authorize(Roles = "1")]
        [HttpDelete("delete")]
        public async Task<IActionResult> Delete(string id)
        {
            var result = await _service.Delete(id);
            return Ok(result);
        }
    }
}
