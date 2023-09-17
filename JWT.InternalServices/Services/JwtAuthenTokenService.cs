using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using JWT.Helper.Config;
using JWT.InternalServices.Interfaces;
using JWT.InternalServices.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualBasic.FileIO;

namespace JWT.InternalServices.Services;

public class JwtAuthenTokenService : IJwtAuthenTokenService
{
    private readonly JwtOption _jwtOption;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public JwtAuthenTokenService(IOptions<JwtOption> jwtOption, IHttpContextAccessor httpContextAccessor)
    {
        _jwtOption = jwtOption.Value;
        _httpContextAccessor = httpContextAccessor;
    }
    private JwtSecurityToken GetToken()
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_jwtOption.Key);

        var token = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString()?.Replace("Bearer ", string.Empty);
        tokenHandler.ValidateToken(token, new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = false,
            ValidateAudience = false,
        }, out SecurityToken validatedToken);
        var jwtToken = (JwtSecurityToken)validatedToken;
        return jwtToken;
    }

    public JwtTokenGetLoginInfoDTO GetLoginInfo()
    {
        var token = GetToken();
        return new JwtTokenGetLoginInfoDTO
        {
            Id = token.Claims.FirstOrDefault(x => x.Type.Equals(ClaimTypes.Sid))?.Value,
            FullName = token.Claims.FirstOrDefault(x => x.Type.Equals(ClaimTypes.Name))?.Value,
            Roles = token.Claims.Where(x => x.Type.Equals(ClaimTypes.Role)).Select(x => x.Value).ToList(),
        };
    }
}