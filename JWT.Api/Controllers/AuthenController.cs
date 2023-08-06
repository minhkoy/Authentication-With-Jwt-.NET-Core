using JWT.Infrastructure.ApiIO;
using JWT.Manager.JwtAuthentication.Request;
using JWT.Manager.JwtAuthentication.Response;
using JWT.Manager.RequestValidators;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using FluentValidation;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace JWT.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenController : ControllerBase
    {
        private readonly IValidator<JwtRegisterModel1> _validator;

        public AuthenController(IValidator<JwtRegisterModel1> validator)
        {
            _validator = validator;
        }
        
        //[AllowAnonymous]
        //[HttpPost("[action]")]
        //[ProducesResponseType(typeof(ApiResult<JwtRegisterResponse1>), (int)HttpStatusCode.OK)]
        //public async Task<ApiResult<JwtRegisterResponse1>> RegisterTemp([FromBody] JwtRegisterRequest1 request)
        //{
        //    //var validator = new JwtRegisterRequestValidator1();
        //    var validationResult = await _validator.ValidateAsync(request.RequestData);
        //    var result = new ApiResult<JwtRegisterResponse1>();

        //    if (!validationResult.IsValid)
        //    {
        //        var modelStateDictionary = new ModelStateDictionary();
        //        foreach(var err in validationResult.Errors)
        //        {
        //            //Console.WriteLine("Error: {0}", err);
        //            modelStateDictionary.AddModelError(err.PropertyName, err.ErrorMessage);
        //            //result.Errors.Add($"Error from {err.PropertyName}: {err.ErrorMessage}");
        //            result.ErrorList = modelStateDictionary;
        //        }
                
                

        //        return FailResult(result);
        //    }

        //    return null;
        //}

        //private ApiResult<JwtRegisterResponse1> FailResult(ApiResult<JwtRegisterResponse1> result)
        //{
        //    return new()
        //    {
        //        Data = null,
        //        StatusCode = HttpStatusCode.OK,
        //        Success = false,
        //        ErrorList = result.ErrorList,
        //    };
        //}
    }
}
