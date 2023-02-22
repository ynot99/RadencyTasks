using FluentValidation;
using HomeTask2.Core.DTOs;

namespace HomeTask2.BusinessLogicLayer.Validators
{
    public class RatingValidator : AbstractValidator<RatingDTO>
    {
        public RatingValidator()
        {
            RuleFor(rating => rating.Score).GreaterThan(0).LessThanOrEqualTo(10).NotEmpty();
            RuleFor(rating => rating.BookId).NotNull();
        }
    }
}
