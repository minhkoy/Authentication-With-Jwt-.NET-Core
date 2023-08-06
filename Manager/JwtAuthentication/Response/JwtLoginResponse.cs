using JWT.Infrastructure.ApiIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JWT.Manager.JwtAuthentication.Response
{
    public class JwtLoginResponse
    {
        public JwtLoginResponse() { }
        public string Token { get; set; }
    }
}
