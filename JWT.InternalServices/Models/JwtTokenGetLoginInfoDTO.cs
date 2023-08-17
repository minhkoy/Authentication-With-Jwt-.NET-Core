using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JWT.InternalServices.Models
{
    public class JwtTokenGetLoginInfoDTO
    {
        public string Id { get; set; }
        public List<string> Roles { get; set; }
    }
}
