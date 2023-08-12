using JWT.InternalServices.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JWT.InternalServices.Interfaces
{
    public interface IJwtAuthenTokenService
    {
        string GetToken(string token);
        JwtTokenGetLoginInfoDTO GetLoginInfo();
    }
}
