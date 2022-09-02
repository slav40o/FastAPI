namespace FastAPI.Layers.Application.Request;

using FastAPI.Layers.Application.Request.Attributes;

[AppQueryRequest]
public abstract record AppQueryRequest<TListItem> : IAppQueryRequest<TListItem>
{
    public int PageSize { get; init; }

    public int PageNumber { get; init; }
}
