using Domain.Entities;
using Domain.Request;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;

namespace Infrastructure.Validators
{
    public class UserDtoValidator : AbstractValidator<User>
    {
        private readonly AppDbContext _context;
        public UserDtoValidator(AppDbContext _context)
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("İsimi doldur");
            RuleFor(x => x.SurName).NotEmpty().WithMessage("Soyismi doldur");
            RuleFor(x => x.Mail).NotEmpty().WithMessage("Mail doldur").MustAsync(async (mail, cancellation) =>
            (!await _context.Users.AnyAsync(u => u.Mail == mail ))).WithMessage("kullandığınız mail mevcut");
        }
    }
}
