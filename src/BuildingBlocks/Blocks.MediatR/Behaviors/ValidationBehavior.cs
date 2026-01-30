using FluentValidation;
using MediatR;

namespace Blocks.MediatR.Behaviors;

public class ValidationBehavior<TRequest, TResponse>
    (IEnumerable<IValidator<TRequest>> validators)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var context = new ValidationContext<TRequest>(request);

        var result = await Task.WhenAll(validators.Select(x => x.ValidateAsync(context, cancellationToken)));

        var failures = result
            .Where(x => x.Errors.Any())
            .SelectMany(x => x.Errors)
            .ToList();

        if (failures.Any())
            throw new ValidationException(failures);

        return await next();
    }
}
