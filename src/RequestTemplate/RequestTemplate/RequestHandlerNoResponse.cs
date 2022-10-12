namespace $rootnamespace$;

using FastAPI.Layers.Application.Handlers;
using FastAPI.Layers.Application.Response;

internal sealed class $safeitemname$RequestHandler : AppRequestHandler<$safeitemname$Request>;
{
    public $safeitemname$RequestHandler() 
    {

    }

    public override Task<AppResponse> HandleRequest($safeitemname$Request request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}