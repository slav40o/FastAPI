namespace FastAPI.Layers.Application.Request;

using FastAPI.Layers.Application.Request.Paging;
using FastAPI.Layers.Application.Response;

using MediatR;

public interface IAppRequest : IRequest<AppResponse>
{
}

public interface IAppRequest<TResponseData> : IRequest<AppResponse<TResponseData>>
{
}

public interface IAppQueryRequest<TListItem> : IRequest<AppResponse<IPageData<TListItem>>>
{
    int PageSize { get; }

    int PageNumber { get; }
}