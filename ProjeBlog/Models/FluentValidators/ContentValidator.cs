using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Model;
using System;
using FluentValidation;

namespace ProjeBlog.Models.FluentValidators
{
    public class ContentValidator:BaseValidator<Content>
    {
        public ContentValidator()
        {
            RuleFor(x => x.Title).NotEmpty().WithMessage(MsgZorunlu)
                .MaximumLength(60).WithMessage(MsgMaxStr(60));

            RuleFor(x => x.CategoryID).NotNull().WithMessage(MsgZorunlu);

            RuleFor(x => x.Entry).NotEmpty().WithMessage(MsgZorunlu);
        }
    }
}
