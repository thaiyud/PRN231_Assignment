using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using PEPRN231_SU24_009909_Service;

namespace PEPRN231_SU24_009909_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PremierLeagueAccountController : Controller
    {

        private readonly PremierLeagueAccountService _account;

        public PremierLeagueAccountController(PremierLeagueAccountService account)
        {
            _account = account;
        }

        [HttpPost("Login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            var user = _account.Login(request.UserEmail, request.UserPassword);
            if (user == null || user.Result == null)
                return Unauthorized();
            return Ok(user.Result);
        }
        public sealed record LoginRequest(string UserEmail, string UserPassword);
    }
}
