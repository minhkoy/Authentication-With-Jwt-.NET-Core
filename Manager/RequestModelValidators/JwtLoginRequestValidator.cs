using FluentValidation;
using JWT.Manager.JwtAuthentication.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JWT.Manager.RequestValidators
{
    public class JwtLoginRequestValidator : AbstractValidator<JwtLoginModel>
    {
        public JwtLoginRequestValidator()
        {
            RuleFor(x => x.UsernameOrEmail).NotEmpty().WithMessage("Username or email is required.");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Password cannot be empty");
        }
    }
}
