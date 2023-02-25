using FluentValidation;
using HomeTask2.Core.DTOs;

namespace HomeTask2.BusinessLogicLayer.Validators
{
    public class ReviewContentValidator : AbstractValidator<ReviewContentDTO>
    {
        public ReviewContentValidator()
        {
            RuleFor(review => review.Message).NotEmpty();
            RuleFor(review => review.Reviewer).NotEmpty();
        }
    }
}
