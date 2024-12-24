using FluentValidation;

namespace ProjeBlog.Models.FluentValidators
{
    public class CategoryValidator:BaseValidator<Category>
    {
        public CategoryValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage(MsgZorunlu)
                .MaximumLength(20).WithMessage(MsgMaxStr(20));

            RuleFor(x => x.Description).NotEmpty().WithMessage(MsgZorunlu)
                .MaximumLength(150).WithMessage(MsgMaxStr(150));
        }
    }
}
