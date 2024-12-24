using FluentValidation;

namespace ProjeBlog.Models.FluentValidators
{
    public class AppUserDetailValidator:BaseValidator<AppUserDetail>
    {
        public AppUserDetailValidator()
        {
            RuleFor(x => x.DateOfBirth).NotEmpty().WithMessage(MsgZorunlu);

            RuleFor(x => x.FirstName).NotEmpty().WithMessage(MsgZorunlu)
                .MaximumLength(50).WithMessage(MsgMaxStr(50));

            RuleFor(x => x.LastName).NotEmpty().WithMessage(MsgZorunlu)
                .MaximumLength(50).WithMessage(MsgMaxStr(50));
        }
    }
}
