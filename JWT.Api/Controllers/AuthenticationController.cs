using JWT.Manager.JwtAuthentication.Response;
using JWT.Manager.JwtAuthentication.Request;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using JWT.Manager.Validators;

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
        [ProducesResponseType(typeof(JwtLoginResponse), 200)]
        public async Task<IActionResult> Login([FromBody] JwtLoginRequest request)
        {
            return Ok(await this.Mediator.Send(request));
        }

        /// <summary>
        /// Login temp uri (fixing MediatR)
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("[action]")]
        [ProducesResponseType(typeof(JwtLoginResponse), (int)HttpStatusCode.OK)]
        public async Task<JwtLoginResponse> LoginTemp([FromBody] JwtLoginRequest request)
        {
            List<string> messages = new();
            if (request is null)
            {
                messages.Add("Input is empty.");
                return new()
                {
                    Token = null,
                    ApiSuccess = false,
                    ApiMessage = messages
                };
            }
            if (string.IsNullOrEmpty(request.UsernameOrEmail) || string.IsNullOrEmpty(request.Password))
            {
                messages.Add("You must fill in username/email and password!");
                return new()
                {
                    Token = null,
                    ApiSuccess = false,
                    ApiMessage = messages
                };
            }
            return new()
            {
                Token = "abcxyz",
                ApiSuccess = true,
                ApiMessage = null
            };
        }

        //[AllowAnonymous]
        //[HttpPost("[action]")]
        //[ProducesResponseType(typeof(JwtRegisterResponse), (int)HttpStatusCode.OK)]
        //public async Task<JwtRegisterResponse> RegisterTemp([FromBody] JwtRegisterRequest request)
        //{
        //    //if (ModelState.IsValid)
        //    //{
        //    //    return new()
        //    //    {
        //    //        ApiSuccess = true,
        //    //        ApiMessage = null,
        //    //    };
        //    //}
        //    //else
        //    //{
        //    //    return new()
        //    //    {
        //    //        ApiSuccess = false,
        //    //        ApiMessage = "Validation fails"
        //    //    };
        //    //}
        //    var validator = new JwtRegisterRequestValidator();
        //    var validationResult = validator.Validate(request);
        //    List<string> messages = new();
        //    if (!validationResult.IsValid)
        //    {
        //        foreach (var failure in validationResult.Errors)
        //        {
        //            messages.Add(failure.ErrorMessage);
        //        }
        //        return new()
        //        {
        //            ApiMessage = 
        //        };
        //    }
        //}
    }
}
