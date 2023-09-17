namespace JWT.Manager.Bases;

public class ValidationException : Exception
{
    public IReadOnlyCollection<ValidationError> Errors { get; set; }
    public ValidationException(IReadOnlyCollection<ValidationError> errors) : base("Validation failed")
    {
        Errors = errors;
    }
}

public record ValidationError(string PropertyName, string ErrorMessage);