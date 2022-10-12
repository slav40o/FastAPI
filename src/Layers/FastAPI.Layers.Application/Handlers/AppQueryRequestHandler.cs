namespace FastAPI.Layers.Application.Handlers;

using FastAPI.Layers.Application.Request;
using FastAPI.Layers.Application.Request.Paging;
using FastAPI.Layers.Application.Response;

using MediatR;

using System.Linq;

public abstract class AppQueryRequestHandler<TRequest, TListItem> : IRequestHandler<TRequest, AppResponse<IPageData<TListItem>>>
    where TRequest : IAppQueryRequest<TListItem>
{
    public async Task<AppResponse<IPageData<TListItem>>> Handle(TRequest request, CancellationToken cancellationToken)
    {
        var itemsQuery = await this.HandleRequest(request, cancellationToken);

        itemsQuery = itemsQuery
            .Skip(request.PageSize * (request.PageNumber - 1))
            .Take(request.PageSize);

        IPageData<TListItem> pageData = new PageData<TListItem>(request.PageNumber, request.PageSize, itemsQuery);

        return AppResponse.Success(string.Empty, pageData);
    }

    public abstract Task<IQueryable<TListItem>> HandleRequest(TRequest request, CancellationToken cancellationToken);
}