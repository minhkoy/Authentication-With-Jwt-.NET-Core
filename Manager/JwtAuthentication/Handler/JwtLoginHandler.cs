using JWT.Manager.JwtAuthentication.Request;
using JWT.Manager.JwtAuthentication.Response;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using JWT.Infrastructure.ApiIO;
using FluentValidation;
using JWT.Data;
using JWT.Data.Interfaces;
using JWT.Domain.Models;
using JWT.Helper.Config;
using JWT.Helper.Extensions;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace JWT.Manager.JwtAuthentication.Handler
{
    public class JwtLoginHandler : RequestHandler<JwtLoginRequest, JwtLoginResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<JwtLoginModel> _validator;
        private readonly JwtDbContext _jwtDbContext;
        private readonly JwtOption _jwtOption;
        private readonly IConfiguration _configuration;
        public JwtLoginHandler(IValidator<JwtLoginModel> validator,
            JwtDbContext jwtDbContext,
            IOptions<JwtOption> jwtOption,
            IConfiguration config,
            IUnitOfWork unitOfWork)
        {
            _validator = validator;
            _jwtDbContext = jwtDbContext;
            _jwtOption = jwtOption.Value;
            _configuration = config;
            _unitOfWork = unitOfWork;
        }

        public override async Task<ApiResult<JwtLoginResponse>> Handle(JwtLoginRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var data = request.RequestData;
                var validationResult = await _validator.ValidateAsync(data);

                if (!validationResult.IsValid)
                {
                    var errors = new ModelStateDictionary();
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
            
                var people = await _unitOfWork.UserInfos
                    .GetQueryable()
                    .FirstOrDefaultAsync(x => x.Username.Equals(data.UsernameOrEmail)
                                              || x.Email.Equals(data.UsernameOrEmail));
                if (people is null)
                {
                    return new()
                    {
                        Data = null,
                        StatusCode = HttpStatusCode.OK,
                        Success = false,
                        ErrorList = new List<string>() {"Username/email or password is incorrect."}
                    };
                }

                var hashedInputPassword = HelperExtensions.HashingWithKey(data.Password, people.SecurityKey);
                if (!hashedInputPassword.Equals(people.HashedPassword))
                {
                    return new()
                    {
                        Data = null,
                        StatusCode = HttpStatusCode.OK,
                        Success = false,
                        ErrorList = new List<string>() {"Username/email or password is incorrect."}
                    };
                }
                
                var token = GenerateJwtForUser(people);
                return new()
                {
                    Data = new()
                    {
                        Token = token
                    },
                    StatusCode = HttpStatusCode.OK,
                    Success = true,
                    ErrorList = null
                };
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return new()
                {
                    Data = null,
                    StatusCode = HttpStatusCode.InternalServerError,
                    Success = false,
                    ErrorList = e
                };
            }
        }
        
        
        private string GenerateJwtForUser(UserInfo user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtOption.Key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var userRoles = _jwtDbContext.RoleUsers
                .Where(x => x.UserId.Equals(user.Id))
                .ToList();
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Sid, user.Id),
                //new Claim(ClaimTypes.NameIdentifier, user.Username),
                // new Claim(ClaimTypes.Email, user.Email),
                // new Claim(ClaimTypes.Name, user.Fullname),
                // new Claim(ClaimTypes.DateOfBirth, user.DateOfBirth.ToString()),
                // new Claim(ClaimTypes.MobilePhone, user.PhoneNumber)
            };

            foreach (var role in userRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role.RoleId));
            }
            
            var token = new JwtSecurityToken(issuer: _jwtOption.Issuer, 
                audience: _jwtOption.Audience,
                claims: claims,
                expires: DateTime.Now.AddMonths(3),
                signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
