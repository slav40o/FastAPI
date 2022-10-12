namespace $rootnamespace$;

using FastAPI.Layers.Application.Handlers;
using FastAPI.Layers.Application.Response;

internal sealed class $safeitemname$RequestHandler : AppRequestHandler<$safeitemname$Request, $responsemodel$>;
{
    public $safeitemname$RequestHandler() 
    {

    }

    public override Task<AppResponse<$responsemodel$>> HandleRequest($safeitemname$Request request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}