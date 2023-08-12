using FluentValidation;
using JWT.Data;
using JWT.Manager.JwtAuthentication.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JWT.Data.Interfaces;

namespace JWT.Manager.RequestModelValidators
{
    public class JwtRegisterRequestValidator : AbstractValidator<JwtRegisterModel>
    {
        //private readonly JwtDbContext _jwtContext;
        private readonly IUnitOfWork _unitOfWork;

        public JwtRegisterRequestValidator(JwtDbContext jwtDbContext, IUnitOfWork unitOfWork)
        {
            //_jwtContext = jwtDbContext;
            _unitOfWork = unitOfWork;
            RuleFor(request => request.Username).NotEmpty().WithMessage("Username is required.");
            RuleFor(request => request.Username).MinimumLength(6)
                .WithMessage("Username must be at least 6 characters.");
            RuleFor(request => request.Username).Must(BeUniqueUsername).WithMessage("Username exists. Choose another one.");
            RuleFor(request => request.Password)
                .Matches("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-0_]).{8,}$")
                .WithMessage("Password must be stronger");
            RuleFor(request => request.Email).EmailAddress().WithMessage("Email should be a valid email address.");
            RuleFor(request => request.Email).Must(BeUniqueEmail).WithMessage("Email exists. Choose another one.");
            RuleFor(request => request.PhoneNumber)//.Cascade(CascadeMode.Continue)
                .Matches("^[0-9]+$").WithMessage("Phone number is in wrong format.")
                .MinimumLength(10).WithMessage("Phone number must be at least 10 numeric characters.")
                .MaximumLength(11).WithMessage("Phone number must be at 11 maximum numeric characters.");
            RuleFor(request => request.DateOfBirth).Must(BeDateTimeString).WithMessage("Date of birth is in wrong format.");
            //RuleFor(request => request.DateOfBirth).
            //RuleFor(request => request.Username).Custom(username => { })
        }

        private bool BeDateTimeString(long? dob)
        {
            if (!dob.HasValue)
            {
                return true;
            }
            return DateTime.TryParseExact(dob.Value.ToString(), "yyyyMMdd", null, System.Globalization.DateTimeStyles.None, out _);
        }

        private bool BeUniqueEmail(string email)
        {
            bool isExistEmail = _unitOfWork.UserInfos
                .GetQueryable().Any(x => x.Email.Equals(email));
            return !isExistEmail;
        }

        private bool BeUniqueUsername(string username)
        {
            var isExistUsername = _unitOfWork.UserInfos
                .GetQueryable().Any(x => x.Username.Equals(username));
            return !isExistUsername;
        }
    }
}