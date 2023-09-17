using FluentValidation;
using JWT.Manager.JwtAuthentication.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JWT.Manager.JwtAuthentication.Validator
{
    public class JwtRegisterRequestValidator1 : AbstractValidator<JwtRegisterModel1>
    {
        public JwtRegisterRequestValidator1()
        {
            RuleFor(request => request.Username).NotEmpty().WithMessage("Username is required.");
            RuleFor(request => request.Username).MinimumLength(6).WithMessage("Username must be at least 6 characters.");
            RuleFor(request => request.Password).Matches("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$");
            RuleFor(request => request.Email).EmailAddress().WithMessage("Email must be a valid email address.");
            RuleFor(request => request.PhoneNumber).MinimumLength(10).MaximumLength(12).Matches("^[0-9]+$").WithMessage("Phone number is invalid.");
            //RuleFor(request => request.EnterpriseId).NotEmpty().
        }
    }
}
