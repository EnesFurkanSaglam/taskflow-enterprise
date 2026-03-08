using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Identity.Application.Commands.Register
{
    public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
    {
        public RegisterCommandValidator() 
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email boş olamaz")
                .EmailAddress().WithMessage("Geçerli bir email addresi giriniz");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Şifre Boş olamaz")
                .MinimumLength(6).WithMessage("Şifre en az 6 karakter olmalı");

            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("Ad boş olamaz")
                .MaximumLength(50).WithMessage("Ad en fazla 50 karakter olabilir");

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Soyad boş olamaz")
                .MaximumLength(50).WithMessage("Soyad en fazla 50 karakter olabilir");
        }
    }
}
