using FluentValidation;
using HomeTask2.Core.DTOs;

namespace HomeTask2.BusinessLogicLayer.Validators
{
    public class RatingScoreValidator : AbstractValidator<RatingScoreDTO>
    {
        public RatingScoreValidator()
        {
            RuleFor(rating => rating.Score)
                .NotEmpty().GreaterThan(0).LessThanOrEqualTo(5);
        }
    }
}
