namespace FastAPI.Layers.Application.Handlers;

using FastAPI.Layers.Application.Request;
using FastAPI.Layers.Application.Resources;
using FastAPI.Layers.Application.Response;

using MediatR;

using System.Threading;
using System.Threading.Tasks;

public abstract class AppRequestHandler<TRequest> : IRequestHandler<TRequest, AppResponse>
    where TRequest : IRequest<AppResponse>, IHaveValidation
{
    public Task<AppResponse> Handle(TRequest request, CancellationToken cancellationToken)
    {
        if (!request.IsValid)
        {
            var response = AppResponse.ValidationFail(
                message: ApplicationMessages.InvalidData,
                validationErrors: request.GetValidationErrors().ToArray());

            return Task.FromResult(response);
        }

        return this.HandleRequest(request, cancellationToken);
    }

    protected AppResponse Success(string message)
        => AppResponse.Success(message);

    protected AppResponse Failure(string message, params AppError[] errors)
        => AppResponse.ValidationFail(message, errors);

    protected AppResponse NotFound(string message)
        => AppResponse.NotFound(message);

    protected AppResponse Unauthorized(string message)
        => AppResponse.Unauthorized(message);

    public abstract Task<AppResponse> HandleRequest(TRequest request, CancellationToken cancellationToken);
}

public abstract class AppRequestHandler<TRequest, TData> : IRequestHandler<TRequest, AppResponse<TData>>
    where TRequest : IRequest<AppResponse<TData>>, IHaveValidation
{
    public Task<AppResponse<TData>> Handle(TRequest request, CancellationToken cancellationToken)
    {
        if (!request.IsValid)
        {
            var response = AppResponse<TData>.ValidationFail<TData>(
                message: ApplicationMessages.InvalidData,
                validationErrors: request.GetValidationErrors().ToArray());

            return Task.FromResult(response);
        }

        return this.HandleRequest(request, cancellationToken);
    }

    protected AppResponse<TData> Success(string message, TData data)
    => AppResponse.Success(message, data);

    protected AppResponse<TData> Failure(string message, params AppError[] errors)
        => AppResponse.ValidationFail<TData>(message, errors);

    protected AppResponse<TData> NotFound(string message)
        => AppResponse.NotFound<TData>(message);

    protected AppResponse<TData> Unauthorized(string message)
        => AppResponse.Unauthorized<TData>(message);

    public abstract Task<AppResponse<TData>> HandleRequest(TRequest request, CancellationToken cancellationToken);
}