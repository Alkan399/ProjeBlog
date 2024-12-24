using FluentValidation;

namespace ProjeBlog.Models.FluentValidators
{
    public class PasswordChangeViewModelValidator:BaseValidator<PasswordChangeViewModel>
    {
        public PasswordChangeViewModelValidator()
        {
            RuleFor(x => x.OldPassword).NotEmpty().WithMessage(MsgZorunlu);

            RuleFor(x => x.NewPassword).NotEmpty().WithMessage(MsgZorunlu)
                .MaximumLength(15).WithMessage(MsgMaxStr(15))
                .MinimumLength(6).WithMessage(MsgMinStr(6))
                .Must((model, newPassword) => newPassword == model.ReNewPassword).WithMessage("Yeni şifre ve tekrarı aynı olmalıdır.");

            RuleFor(x => x.ReNewPassword).NotEmpty().WithMessage(MsgZorunlu)
                .MaximumLength(15).WithMessage(MsgMaxStr(15))
                .MinimumLength(6).WithMessage(MsgMinStr(6))
                .Must((model, ReNewPassword) => ReNewPassword == model.NewPassword).WithMessage("Yeni şifre ve tekrarı aynı olmalıdır.");

        }

    }
}
