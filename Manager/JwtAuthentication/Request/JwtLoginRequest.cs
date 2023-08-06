using JWT.Manager.JwtAuthentication.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JWT.Infrastructure.ApiIO;

namespace JWT.Manager.JwtAuthentication.Request
{
    public class JwtLoginRequest : RequestBase<JwtLoginModel, JwtLoginResponse>
    {
    }

    public class JwtLoginModel
    {
        public string UsernameOrEmail { get; set; }
        public string Password { get; set; }
    }
}
