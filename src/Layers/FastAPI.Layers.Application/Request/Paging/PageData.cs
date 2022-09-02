namespace FastAPI.Layers.Application.Request.Paging;

public record PageData<TResponseData>(int PageNumber, int PageSize, IQueryable<TResponseData> Items) 
    : IPageData<TResponseData>;
