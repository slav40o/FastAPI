namespace FastAPI.Features.Identity.Application.Requests.Users.ChangePassword;

using FastAPI.Layers.Application.Handlers;
using FastAPI.Layers.Application.Response;

public sealed class ChangePasswordRequestHandler : AppRequestHandler<ChangePasswordRequest>
{
    public ChangePasswordRequestHandler()
    {
    }

    public override Task<AppResponse> HandleRequest(ChangePasswordRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}