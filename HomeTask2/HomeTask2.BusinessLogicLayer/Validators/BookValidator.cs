using FluentValidation;
using HomeTask2.Core.DTOs;

namespace HomeTask2.BusinessLogicLayer.Validators
{
    public class BookValidator : AbstractValidator<BookDTO>
    {
        public BookValidator()
        {
            RuleFor(book => book.Title).NotEmpty().WithMessage("Title is required");
            RuleFor(book => book.Cover)
                .NotEmpty().WithMessage("Cover is required")
                // https://stackoverflow.com/questions/8571501/how-to-check-whether-a-string-is-base64-encoded-or-not
                .Matches(@"^data:image\/(png|jpg|jpeg);base64,([A-Za-z0-9+/]{4})*([A-Za-z0-9+/]{3}=|[A-Za-z0-9+/]{2}==)?$")
                .WithMessage("Cover doesn't match base64 format.");
            RuleFor(book => book.Content).NotEmpty().WithMessage("Content is required");
            RuleFor(book => book.Genre).NotEmpty().WithMessage("Genre is required");
            RuleFor(book => book.Author).NotEmpty().WithMessage("Author is required");
        }
    }
}