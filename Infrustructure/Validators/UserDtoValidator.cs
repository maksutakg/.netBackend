using Domain.Entities;
using FluentValidation;
using FluentValidation.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Validators
{
    public class UserDtoValidator : AbstractValidator<UserDto>
    {
        public UserDtoValidator()
        {
             RuleFor(x => x.name).NotEmpty().WithMessage("İsimi doldur");
             RuleFor(x => x.surName).NotEmpty().WithMessage("Soyismi doldur");
             RuleFor(x => x.Note).NotEmpty().MaximumLength(900).WithMessage("900 karekter ");
             RuleFor(x => x.Mail).NotEmpty().WithMessage("Mail doldur");
             
        }
    }
}
