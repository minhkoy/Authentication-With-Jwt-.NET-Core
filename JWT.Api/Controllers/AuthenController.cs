using JWT.Infrastructure.ApiIO;
using JWT.Manager.JwtAuthentication.Request;
using JWT.Manager.JwtAuthentication.Response;
using JWT.Manager.RequestValidators;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace JWT.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenController : ControllerBase
    {
        [AllowAnonymous]
        [HttpPost("[action]")]
        [ProducesResponseType(typeof(ApiResult<JwtRegisterResponse1>), (int)HttpStatusCode.OK)]
        public async Task<ApiResult<JwtRegisterResponse1>> RegisterTemp([FromBody] JwtRegisterRequest1 request)
        {
            var validator = new JwtRegisterRequestValidator1();
            var validationResult = await validator.ValidateAsync(request.RequestData);
            var result = new ApiResult<JwtRegisterResponse1>();

            if (!validationResult.IsValid)
            {
                foreach(var err in validationResult.Errors)
                {
                    Console.WriteLine("Error: {0}", err);
                    result.Errors.Add(err.ErrorMessage);
                }

                return FailResult(result);
            }
        }

        private ApiResult<JwtRegisterResponse1> FailResult(ApiResult<JwtRegisterResponse1> result)
        {
            return new()
            {
                Data = null,
                StatusCode = HttpStatusCode.OK,
                Success = false,
                Errors = result.Errors
            };
        }
    }
}
