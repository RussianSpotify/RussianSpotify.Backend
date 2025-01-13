#region

using FluentValidation;
using MediatR;

#endregion

namespace RussianSpotify.API.Core.Common.Behaviors;

/// <summary>
///     Подключение пайплайна валидации
/// </summary>
/// <typeparam name="TRequest"></typeparam>
/// <typeparam name="TResponse"></typeparam>
// TODO: Либо переписать Commands и Queries с использованием этого, либо убрать(лучше переписать), т.к. логика обработки данных мешается с валидацией
public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators) => _validators = validators;

    public Task<TResponse> Handle(TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        var context = new ValidationContext<TRequest>(request);

        var failures = _validators
            .Select(validator => validator.Validate(context))
            .SelectMany(result => result.Errors)
            .Where(failure => failure is not null)
            .ToList();

        return failures.Count == 0
            ? next()
            : throw new ValidationException(failures);
    }
}