namespace FastAPI.Layers.Application.Request.Paging;

public interface IPageData<TResponseData>
{
    int PageNumber { get; }

    int PageSize { get; }

    IQueryable<TResponseData> Items { get; }
}
