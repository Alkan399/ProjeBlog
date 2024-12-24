using FluentValidation;

namespace ProjeBlog.Models.FluentValidators
{
    public class AppUserValidator:BaseValidator<AppUser>
    {
        public AppUserValidator()
        {
            RuleFor(x => x.Email).NotEmpty().WithMessage(MsgZorunlu)
                .EmailAddress().WithMessage("Lütfen e-posta formatını doğru giriniz.");

            RuleFor(x => x.UserName).NotEmpty().WithMessage(MsgZorunlu)
                .MaximumLength(15).WithMessage(MsgMaxStr(15));

            RuleFor(x => x.Password).NotEmpty().WithMessage(MsgZorunlu)
                .MaximumLength(15).WithMessage(MsgMaxStr(15))
                .MinimumLength(6).WithMessage(MsgMinStr(6));

            RuleFor(x => x.Role).NotNull().WithMessage(MsgZorunlu);

            RuleFor(x => x.AppUserDetail)
            .SetValidator(new AppUserDetailValidator());
        }
    }
}
