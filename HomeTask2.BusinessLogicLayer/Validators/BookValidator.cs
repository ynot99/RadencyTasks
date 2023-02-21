using FluentValidation;
using HomeTask2.BusinessLogicLayer.Models;

namespace HomeTask2.BusinessLogicLayer.Validators
{
    public class BookValidator : AbstractValidator<BookModel>
    {
        public BookValidator()
        {
            RuleFor(book => book.Title).NotEmpty();
            RuleFor(book => book.Cover).NotEmpty();
            RuleFor(book => book.Content).NotEmpty();
        }
    }
}
