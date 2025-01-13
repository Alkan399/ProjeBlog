using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Model;
using System;
using FluentValidation;
using System.Linq;
namespace ProjeBlog.Models.FluentValidators
{
    public class ContentSetElementsValidator : BaseValidator<ContentSetElement>
    {
        public ContentSetElementsValidator()
        {
            RuleFor(x => x.ElementID).NotEmpty().WithMessage(MsgZorunlu)
                .MaximumLength(60).WithMessage(MsgMaxStr(60));

            RuleFor(x => x.ShowCount).NotNull().WithMessage(MsgZorunlu);

            RuleFor(x => x)
            .Must(x => new[] { x.MostPopular, x.Recent, x.Custom }.Count(flag => flag) <= 1)
            .WithMessage(MsgGeneric);
        }
    }
}
