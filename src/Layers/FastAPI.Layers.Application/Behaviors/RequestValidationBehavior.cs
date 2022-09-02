namespace FastAPI.Layers.Application.Behaviors;

using FluentValidation;

using MediatR;

using Request;

using Response;

using System.Threading;
using System.Threading.Tasks;

public class RequestValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>, IHaveValidation
{
    private readonly IEnumerable<IValidator<TRequest>> validators;

    public RequestValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        this.validators = validators;
    }

    public Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
    {
        var context = new ValidationContext<TRequest>(request);

        var errors = this
            .validators
            .Select(v => v.Validate(context))
            .SelectMany(result => result.Errors)
            .Where(f => f is not null)
            .GroupBy(f => f.PropertyName)
            .Select(g => new AppError(g.Key, g.Select(f => f.ErrorMessage)))
            .ToArray();

        if (errors is not null && errors.Any())
        {
            request.AddValidationErrors(errors);
        }

        return next();
    }
}
