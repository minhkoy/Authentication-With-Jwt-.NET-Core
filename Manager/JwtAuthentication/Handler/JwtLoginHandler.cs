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
    }
}
