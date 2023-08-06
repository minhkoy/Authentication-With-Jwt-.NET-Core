using FluentValidation;
using JWT.Data;
using JWT.Helper.Config;
using JWT.Infrastructure.ApiIO;
using JWT.Manager.JwtAuthentication.Request;
using JWT.Manager.JwtAuthentication.Response;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using JWT.Domain.Models;
using JWT.Helper.Extensions;
using JWT.Helper.HelperProperties;
using Microsoft.EntityFrameworkCore;

namespace JWT.Manager.JwtAuthentication.Handler
{
    public class JwtRegisterHandler : RequestHandler<JwtRegisterRequest, JwtRegisterResponse>
    {
        private readonly JwtDbContext _jwtDbContext;
        private readonly IValidator<JwtRegisterModel> _validator;
        private readonly JwtOption _jwtOption;

        public JwtRegisterHandler(JwtDbContext jwtDbContext,
            IValidator<JwtRegisterModel> validator,
            IOptions<JwtOption> options)
        {
            _jwtDbContext = jwtDbContext;
            _validator = validator;
            _jwtOption = options.Value;
        }
        public override async Task<ApiResult<JwtRegisterResponse>> Handle(JwtRegisterRequest request, CancellationToken cancellationToken)
        {
            var data = request.RequestData;
            var validationResult = await _validator.ValidateAsync(data);
            var errors = new ModelStateDictionary();
            if (!validationResult.IsValid)
            {
                //var errors = new ModelStateDictionary();
                foreach (var error in validationResult.Errors)
                {
                    errors.AddModelError(error.PropertyName, error.ErrorMessage);
                }

                return new()
                {
                    Data = null,
                    StatusCode = HttpStatusCode.OK,
                    Success = false,
                    ErrorList = errors
                };
            }

            var key = HelperExtensions.GetRandomKey(Constants.SECURITY_KEY_LENGTH);
            UserInfo newUser = new()
            {
                Username = data.Username,
                Email = data.Email,
                SecurityKey = key,
                HashedPassword = HelperExtensions.HashingWithKey(data.Password, key),
                Fullname = data.Fullname,
                DateOfBirth = data.DateOfBirth,
                Address = data.Address,
                PhoneNumber = data.PhoneNumber,
                IsEmailConfirmed = false
            };

            _jwtDbContext.Add(newUser);
            await _jwtDbContext.SaveChangesAsync();
            
            return new()
            {
                Data = null,
                StatusCode = HttpStatusCode.OK,
                Success = true,
                ErrorList = null
            };
            
        }     
    }
}
