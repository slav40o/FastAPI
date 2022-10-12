using FastAPI.Layers.Application.Request;

namespace FastAPI.Layers.Application.Services;

public interface IUrlProvider
{
    string ApiUrl { get; }

    string ClientUrl { get; }

    string GetRequestUrl<TRequest>()
        where TRequest : AppRequest;

    string GetRequestUrl<TRequest>(object args)
        where TRequest : AppRequest;

    string GetRequestUrl<TRequest, TResponseModel>()
        where TRequest : AppRequest<TResponseModel>;

    string GetRequestUrl<TRequest, TResponseModel>(object args)
        where TRequest : AppRequest<TResponseModel>;
}
