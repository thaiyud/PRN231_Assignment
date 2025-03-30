using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using PEPRN231_SU24_009909_Repo.Models;
using PEPRN231_SU24_009909_Service;

namespace PEPRN231_SU24_009909_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FootballClubController : Controller
    {
        private readonly FootballClubService _service;

        public FootballClubController(FootballClubService service)
        {
            _service = service;
        }
        [Authorize(Roles = "1")]
        [HttpGet("get")]
        public async Task<IActionResult> Get()
        {
            var result = await _service.Get();
            return Ok(result);
        }
        [Authorize(Roles = "1")]
        [HttpGet("getById")]
        public async Task<IActionResult> GetById(string id)
        {
            var result = await _service.GetById(id);
            return Ok(result);
        }

       
    }
}
