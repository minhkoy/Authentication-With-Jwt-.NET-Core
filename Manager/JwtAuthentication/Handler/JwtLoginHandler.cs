using JWT.Manager.JwtAuthentication.Request;
using JWT.Manager.JwtAuthentication.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JWT.Manager.JwtAuthentication.Handler
{
    public class JwtLoginHandler : IRequestHandler<JwtLoginRequest, JwtLoginResponse>
    {
        public async Task<JwtLoginResponse> Handle(JwtLoginRequest request, CancellationToken cancellationToken)
        {
            if (request is null)
            {
                return new()
                {
                    Token = null,
                    ApiSuccess = false,
                    ApiMessage = "Input is empty."
                };
            }
            if (string.IsNullOrEmpty(request.UsernameOrEmail) || string.IsNullOrEmpty(request.Password))
            {
                return new()
                {
                    Token = null,
                    ApiSuccess = false,
                    ApiMessage = "You must fill in username/email and password!"
                };
            }
            return new()
            {
                Token = "abcxyz",
                ApiSuccess = true,
                ApiMessage = null
            };
        }
    }
}
