using Microsoft.AspNetCore.Mvc;

namespace FunBooksAndVideos.Api.Models.ValidationErrors;

public class ValidationProblemDetails: ProblemDetails
{
    public IDictionary<string, string[]>? Errors { get; set; } = new Dictionary<string, string[]>();
}
