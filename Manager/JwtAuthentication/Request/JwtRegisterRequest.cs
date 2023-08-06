using JWT.Infrastructure.ApiIO;
using JWT.Manager.JwtAuthentication.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JWT.Manager.JwtAuthentication.Request
{
    public class JwtRegisterRequest : RequestBase<JwtRegisterModel, JwtRegisterResponse>
    {
        //public JwtRegisterRequest() { }
    }

    public class JwtRegisterModel
    {
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
