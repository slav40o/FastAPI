namespace FastAPI.Layers.Presentation.Endpoints;

using Application.Request;
using Application.Request.Attributes;
using Application.Request.Paging;

using FastAPI.Layers.Presentation.Result;

using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

using System;
using System.Reflection;
using System.Text;

public static class MediatrEndpointExtensions
{
    private const string PathDelimeter = "/";
    private static readonly HashSet<string> PrivatePathWords = new() { "api" };

    public static WebApplication MediateRequest<TRequest>(this WebApplication app, string path)
        where TRequest : IAppRequest
    {
        var requestType = typeof(TRequest);

        app.InitRouteBuilder<TRequest>(path, GetRequestType(requestType))
           .SetupBuilderMetaData(path, requestType);

        return app;
    }

    public static WebApplication MediateRequest<TRequest, TResponseData>(this WebApplication app, string path)
        where TRequest : IAppRequest<TResponseData>
    {
        var requestType = typeof(TRequest);
        var responseType = typeof(DataResult<TResponseData>);

        app.InitRouteBuilder<TRequest, TResponseData>(path, GetRequestType(requestType))
           .SetupBuilderMetaData(path, requestType, responseType);

        return app;
    }

    public static WebApplication MediateQueryRequest<TRequest, TListItem>(this WebApplication app, string path)
        where TRequest : IAppQueryRequest<TListItem>
    {
        var requestType = typeof(TRequest);
        var responseType = typeof(DataResult<IPageData<TListItem>>);

        app.InitQueryRouteBuilder<TRequest, TListItem>(path)
           .SetupBuilderMetaData(path, requestType, responseType);

        return app;
    }

    private static RouteHandlerBuilder SetupBuilderMetaData(this RouteHandlerBuilder builder, string path, Type requestType, Type? responseType = null)
    {
        var appRequestType = GetRequestType(requestType);

        builder = builder
            .WithName(requestType.Name)
            .WithTags(GetRequestTag(path));

        if (appRequestType == AppRequestTypes.Get)
        {
            builder = builder
                .Produces(StatusCodes.Status200OK, responseType);
        }
        else if (appRequestType == AppRequestTypes.Create)
        {
            builder = builder
                .Produces(StatusCodes.Status200OK, responseType)
                .Produces(StatusCodes.Status400BadRequest, typeof(ErrorResult));
        }
        else if (appRequestType == AppRequestTypes.Update)
        {
            builder = builder
                .Produces(StatusCodes.Status200OK, responseType)
                .Produces(StatusCodes.Status404NotFound)
                .Produces(StatusCodes.Status400BadRequest, typeof(ErrorResult));
        }

        return ApplyAuthorization(builder, requestType.GetCustomAttribute<AuthorizeAttribute>());
    }

    private static AppRequestTypes GetRequestType(Type requestType)
    {
        var requestAttribute = requestType
            .GetCustomAttribute<AppRequestAttribute>();

        return requestAttribute?.Type ?? AppRequestTypes.Get;
    }

    private static RouteHandlerBuilder InitRouteBuilder<TRequest>(this WebApplication app, string path, AppRequestTypes requestType)
            where TRequest: IAppRequest
    {
        return requestType switch
        {
            AppRequestTypes.Get => app.MapGet(path, 
                async (IMediator mediator, [AsParameters] TRequest request, CancellationToken cancellationToken) 
                    => await mediator.Send(request, cancellationToken).ToIResult()),
            AppRequestTypes.Create => app.MapPost(path,
                async (IMediator mediator, TRequest request, CancellationToken cancellationToken)
                    => await mediator.Send(request, cancellationToken).ToIResult()),
            AppRequestTypes.Update => app.MapPut(path,
                async (IMediator mediator, TRequest request, CancellationToken cancellationToken)
                    => await mediator.Send(request, cancellationToken).ToIResult()),
            AppRequestTypes.Delete => app.MapDelete(path,
                async (IMediator mediator, TRequest request, CancellationToken cancellationToken)
                    => await mediator.Send(request, cancellationToken).ToIResult()),
            _ => throw new ArgumentException("Invalid endpoint access type!"),
        };
    }

    private static RouteHandlerBuilder InitRouteBuilder<TRequest, TResponseData>(this WebApplication app, string path, AppRequestTypes requestType)
            where TRequest : IAppRequest<TResponseData>
    {
        return requestType switch
        {
            AppRequestTypes.Get => app.MapGet(path,
                async (IMediator mediator, [AsParameters] TRequest request, CancellationToken cancellationToken)
                    => await mediator.Send(request, cancellationToken).ToIResult()),
            AppRequestTypes.Create => app.MapPost(path,
                async (IMediator mediator, TRequest request, CancellationToken cancellationToken)
                    => await mediator.Send(request, cancellationToken).ToIResult()),
            AppRequestTypes.Update => app.MapPut(path,
                async (IMediator mediator, TRequest request, CancellationToken cancellationToken)
                    => await mediator.Send(request, cancellationToken).ToIResult()),
            AppRequestTypes.Delete => app.MapDelete(path,
                async (IMediator mediator, TRequest request, CancellationToken cancellationToken)
                    => await mediator.Send(request, cancellationToken).ToIResult()),
            _ => throw new ArgumentException("Invalid endpoint access type!"),
        };
    }

    private static RouteHandlerBuilder InitQueryRouteBuilder<TRequest, TListItem>(this WebApplication app, string path)
        where TRequest : IAppQueryRequest<TListItem>
    {
        return app.MapGet(path,
                async (IMediator mediator, [AsParameters] TRequest request, CancellationToken cancellationToken)
                    => await mediator.Send(request, cancellationToken).ToIResult());
    }

    private static RouteHandlerBuilder ApplyAuthorization(this RouteHandlerBuilder builder, AuthorizeAttribute? authAttribute)
    {
        if (authAttribute is null)
        {
            return builder;
        }

        if (authAttribute.Policy is not null)
        {
            return builder.RequireAuthorization(authAttribute.Policy)
                .Produces(StatusCodes.Status401Unauthorized);
        }

        return builder.RequireAuthorization()
            .Produces(StatusCodes.Status401Unauthorized);
    }

    private static string GetRequestTag(string path)
    {
        var pathParts = path
            .Split(PathDelimeter, StringSplitOptions.RemoveEmptyEntries)
            .Where(p => !PrivatePathWords.Contains(p))
            .ToArray();

        if (pathParts.Length == 0)
        {
            return string.Empty;
        }

        if (pathParts.Length == 1)
        {
            return pathParts[0].Capitalize();
        }

        var tag = new StringBuilder();
        for (int i = 0; i < pathParts.Length - 1; i++)
        {
            tag.Append(pathParts[i].Capitalize());
        }

        return tag.ToString();
    }
}