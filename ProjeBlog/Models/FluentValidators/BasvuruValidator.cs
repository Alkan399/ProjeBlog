using FluentValidation;
using FluentValidation.Validators;

namespace ProjeBlog.Models.FluentValidators
{
    public class BasvuruValidator:BaseValidator<Basvuru>
    {
        public BasvuruValidator()
        {
            RuleFor(x => x.DateOfBirth).NotEmpty().WithMessage(MsgZorunlu);

            RuleFor(x => x.FirstName).NotEmpty().WithMessage(MsgZorunlu)
                .MaximumLength(50).WithMessage(MsgMaxStr(50));

            RuleFor(x => x.LastName).NotEmpty().WithMessage(MsgZorunlu)
                .MaximumLength(50).WithMessage(MsgMaxStr(50));

            RuleFor(x => x.Email).NotEmpty().WithMessage(MsgZorunlu)
                .EmailAddress().WithMessage("Lütfen e-posta formatını doğru giriniz.");

            RuleFor(x => x.Description).NotEmpty().WithMessage(MsgZorunlu);

        }
    }
}
