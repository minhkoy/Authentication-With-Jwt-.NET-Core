using JWT.Manager.JwtAuthentication.Response;
using JWT.Manager.JwtAuthentication.Request;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using JWT.Infrastructure.ApiIO;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace JWT.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        protected IMediator Mediator { get; }
        private readonly IConfiguration _config;
        public AuthenticationController(IConfiguration config,
            IMediator mediator)
        {
            _config = config;
            Mediator = mediator;
        }

        [AllowAnonymous]
        [HttpPost("[action]")]
        [ProducesResponseType(typeof(ApiResult<JwtLoginResponse>), 200)]
        public async Task<IActionResult> Login([FromBody] JwtLoginRequest request)
        {
            return Ok(await this.Mediator.Send(request));
        }

        [AllowAnonymous]
        [HttpPost("[action]")]
        [ProducesResponseType(typeof(ApiResult<JwtRegisterResponse>), 200)]
        public async Task<IActionResult> Register([FromBody] JwtRegisterRequest request)
        {
            return Ok(await this.Mediator.Send(request));
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("[action]")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        public bool Test()
        {
            return true;
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("[action]")]
        [ProducesResponseType(typeof(JwtGetUserInfoResponse), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetUserInfo()
        {
            return Ok(await this.Mediator.Send(new JwtGetUserInfoRequest()));
        }
    }
}
