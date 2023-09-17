using FluentValidation;
using JWT.Data.Interfaces;
using JWT.Domain.Models;
using JWT.Helper.Extensions;
using JWT.Manager.JwtAuthentication.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JWT.Manager.JwtAuthentication.Validator
{
    public class JwtLoginRequestValidator : AbstractValidator<JwtLoginModel>
    {
        private readonly IUnitOfWork _unitOfWork;
        private UserInfo UserInfo { get; set; }
        public JwtLoginRequestValidator(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            UserInfo = null;
            RuleFor(x => x.UsernameOrEmail).NotEmpty().WithMessage("Username or email is required.");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Password cannot be empty");
            RuleFor(x => x).Must(IsExist).WithName("Functional").WithMessage("Username or password is incorrect");

        }

        private bool IsExist(JwtLoginModel model)
        {
            var account = _unitOfWork.UserInfos
                .GetQueryable()
                .FirstOrDefault(x => x.Username.Equals(model.UsernameOrEmail) || x.Email.Equals(model.UsernameOrEmail));
            if (account is null)
            {
                return false;
            }
            UserInfo = account;
            var isCorrectPassword = _unitOfWork.UserInfos
                .GetQueryable()
                .Any(x => x.HashedPassword.Equals(HelperExtensions.HashingWithKey(model.Password, account.SecurityKey)));
            return isCorrectPassword;
        }
    }
}
