﻿using JWT.Manager.JwtAuthentication.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JWT.Manager.JwtAuthentication.Request
{
    public class JwtLoginRequest : IRequest<JwtLoginResponse>
    {
        public string UsernameOrEmail { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
