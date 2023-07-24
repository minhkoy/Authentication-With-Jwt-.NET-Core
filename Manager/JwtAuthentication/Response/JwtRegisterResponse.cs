using JWT.Infrastructure.ApiIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JWT.Manager.JwtAuthentication.Response
{
    public class JwtRegisterResponse : ApiBaseResponse
    {
        public string Id { get; set; }
        public string SecurityKey { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Fullname { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public long? DateOfBirth { get; set; }
        public string EnterpriseId { get; set; }
    }
}
