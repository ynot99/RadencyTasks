using FluentValidation;
using HomeTask2.Core.DTOs;

namespace HomeTask2.BusinessLogicLayer.Validators
{
    public class BookValidator : AbstractValidator<BookDTO>
    {
        public BookValidator()
        {
            RuleFor(book => book.Title).NotEmpty();
            RuleFor(book => book.Cover).NotEmpty();
            RuleFor(book => book.Content).NotEmpty();
        }
    }
}
