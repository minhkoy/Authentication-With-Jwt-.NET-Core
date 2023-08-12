using JWT.Infrastructure.ApiIO;
using JWT.Manager.JwtAuthentication.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JWT.Manager.JwtAuthentication.Request
{
    public class JwtGetUserInfoRequest : RequestNoInput<JwtGetUserInfoResponse>
    {
        
    }
}
