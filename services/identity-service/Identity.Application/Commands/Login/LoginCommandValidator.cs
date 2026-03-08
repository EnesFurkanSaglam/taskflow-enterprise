using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Identity.Application.Commands.Login
{
    public class LoginCommandValidator : AbstractValidator<LoginCommand>

    {
        public LoginCommandValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email boş olamaz")
                .EmailAddress().WithMessage("Geçerli bir email adresi giriniz");
                
            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Şifre boş olamaz");
        }
    }
}
