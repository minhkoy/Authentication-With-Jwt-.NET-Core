using FluentValidation;
using JWT.Infrastructure.ApiIO;
using MediatR;

namespace JWT.Manager.Bases;

public class ValidationBehavior<TRequestModel, TResponse> : IPipelineBehavior<TRequestModel, TResponse>
     //where TRequest : RequestBase<TRequestModel, TResponse>
{
    private readonly IEnumerable<IValidator<TRequestModel>> _validators;

    public ValidationBehavior(IEnumerable<IValidator<TRequestModel>> validators)
    {
        _validators = validators;
    }
    public async Task<TResponse> Handle(TRequestModel request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        //Validate
        var context = new ValidationContext<TRequestModel>(request);
        var errors = _validators.Select(validator => validator.Validate(context))
            .Where(result => !result.IsValid)
            .SelectMany(result => result.Errors)
            .Select(failure => 
                new ValidationError(PropertyName: failure.PropertyName, ErrorMessage: failure.ErrorMessage)).ToList();
        if (errors.Any())
        {
            throw new ValidationException(errors);
        }
        var response = await next();
        return response;
    }
}