using Domain.Entities;
using Domain.Request;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Validators
{
    public class NoteDtoValidator : AbstractValidator<Note>
    {
        public NoteDtoValidator()
        {
            RuleFor(u => u.text).NotEmpty().WithMessage("text doldur ").MaximumLength(99).WithMessage("99 karekter");
            RuleFor(u => u.UserId).NotEmpty().WithMessage("User ıd girin");
        }
    }
}
