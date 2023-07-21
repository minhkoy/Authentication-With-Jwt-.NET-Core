using JWT.Manager.JwtAuthentication.Response;
using JWT.Manager.JwtAuthentication.Request;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JWT.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private IMediator Mediator { get; set; }
        private readonly IConfiguration _config;
        public AuthenticationController(IConfiguration config,
            IMediator mediator)
        {
            _config = config;
            Mediator = mediator;
        }

        [AllowAnonymous]
        [HttpPost("[action]")]
        [ProducesResponseType(typeof(JwtLoginResponse), 200)]
        public async Task<IActionResult> Login([FromBody] JwtLoginRequest request)
        {
            return Ok(await this.Mediator.Send(request));
        }
    }
}
