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

    public abstract Task<AppResponse<TData>> HandleRequest(TRequest request, CancellationToken cancellationToken);
}