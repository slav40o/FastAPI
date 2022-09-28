﻿namespace FastAPI.Features.Identity.Application;

using FastAPI.Layers.Application;

using Microsoft.Extensions.DependencyInjection;

public static class IdentityApplicationConfigurations
{
    public static IServiceCollection AddIdentityApplicationLayer(this IServiceCollection services)
            => services.AddApplicationLayer(typeof(IdentityApplicationConfigurations).Assembly);
}
