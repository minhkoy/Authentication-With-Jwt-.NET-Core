using JWT.Data.Interfaces;
using JWT.Helper.Config;
using JWT.Infrastructure.ApiIO;
using JWT.InternalServices.Interfaces;
using JWT.Manager.JwtAuthentication.Request;
using JWT.Manager.JwtAuthentication.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace JWT.Manager.JwtAuthentication.Handler
{
    public class JwtGetUserInfoHandler : RequestHandler<JwtGetUserInfoRequest, JwtGetUserInfoResponse>
    {
        private readonly JwtOption _jwtOption;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IJwtAuthenTokenService _jwtAuthenTokenService;
        public JwtGetUserInfoHandler(IOptions<JwtOption> options, IHttpContextAccessor httpContextAccessor,
            IUnitOfWork unitOfWork,
            IJwtAuthenTokenService jwtAuthenTokenService)
        {
            _jwtOption = options.Value;
            _httpContextAccessor = httpContextAccessor;
            _unitOfWork = unitOfWork;
            _jwtAuthenTokenService = jwtAuthenTokenService;
        }
        public override async Task<ApiResult<JwtGetUserInfoResponse>> Handle(JwtGetUserInfoRequest request, CancellationToken cancellationToken)
        {
            //var tokenHandler = new JwtSecurityTokenHandler();
            //var key = Encoding.ASCII.GetBytes(_jwtOption.Key);

            //var token = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", string.Empty);
            //tokenHandler.ValidateToken(token, new TokenValidationParameters
            //{
            //    ValidateIssuerSigningKey = true,
            //    IssuerSigningKey = new SymmetricSecurityKey(key),
            //    ValidateIssuer = false,
            //    ValidateAudience = false,
            //}, out SecurityToken validatedToken);
            //var jwtToken = (JwtSecurityToken)validatedToken;
            //var userId = jwtToken.Claims.FirstOrDefault(x => x.Type.Equals(ClaimTypes.Sid))?.Value;

            //var user = await _unitOfWork.UserInfos.FindAsync(userId);

            //return new()
            //{
            //    Data = new()
            //    {
            //        Token = userId
            //    },
            //    StatusCode = System.Net.HttpStatusCode.OK,
            //    Success = true,
            //    ErrorList = null
            //};
            var userInfoFromToken = _jwtAuthenTokenService.GetLoginInfo();
            var user = await _unitOfWork.UserInfos.FindAsync(userInfoFromToken.Id);
            return new()
            {
                Data = new()
                {
                    Id = user.Id,
                    Username = user.Username,
                    Roles = userInfoFromToken.Roles
                },
                StatusCode = System.Net.HttpStatusCode.OK,
                Success = true,
                ErrorList = null
            };
        }
    }
}
