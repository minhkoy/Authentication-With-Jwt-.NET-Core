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
        public Task<JwtLoginResponse> Handle(JwtLoginRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
