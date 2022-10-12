namespace $rootnamespace$;

using FastAPI.Layers.Application.Handlers;

internal sealed class $safeitemname$RequestHandler : AppQueryRequestHandler<$safeitemname$Request, $responsemodel$>;
{
    public $safeitemname$RequestHandler() 
    {

    }

    public override Task<IQueryable<$responsemodel$>> HandleRequest($safeitemname$Request request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}