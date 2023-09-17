using JWT.Infrastructure.ApiIO;
using JWT.Manager.JwtAuthentication.Request;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JWT.Manager.JwtAuthentication.Handler
{
    public class JwtLogoutHandler : IRequestHandler<JwtLogoutRequest, bool>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public JwtLogoutHandler(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<bool> Handle(JwtLogoutRequest request, CancellationToken cancellationToken)
        {
            var context = _httpContextAccessor.HttpContext;
            context.Response.Cookies.Delete("LoginToken");
            return true;
        }
    }
}
