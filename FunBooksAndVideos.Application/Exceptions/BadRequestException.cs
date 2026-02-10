using FluentValidation.Results;

namespace FunBooksAndVideos.Application.Exceptions;

public class BadRequestException: Exception
{
    public IEnumerable<string>? ValidationErrors { get; private set; }

    public BadRequestException(string message) : base(message)
    {
    }

    public BadRequestException(string message, ValidationResult validationResult)
        : base(message)
    {
        ValidationErrors = validationResult.Errors?.Select(e => e.ErrorMessage).ToList();
    }
}
