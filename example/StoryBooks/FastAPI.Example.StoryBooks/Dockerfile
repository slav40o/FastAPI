#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["example/StoryBooks/FastAPI.Example.StoryBooks/FastAPI.Example.StoryBooks.csproj", "example/StoryBooks/FastAPI.Example.StoryBooks/"]
COPY ["src/Features/Identity/FastAPI.Features.Identity/FastAPI.Features.Identity.csproj", "src/Features/Identity/FastAPI.Features.Identity/"]
COPY ["src/Features/Identity/FastAPI.Features.Identity.Infrastructure/FastAPI.Features.Identity.Infrastructure.csproj", "src/Features/Identity/FastAPI.Features.Identity.Infrastructure/"]
COPY ["src/Layers/FastAPI.Layers.Infrastructure.Persistence.Postgre/FastAPI.Layers.Infrastructure.Persistence.Postgre.csproj", "src/Layers/FastAPI.Layers.Infrastructure.Persistence.Postgre/"]
COPY ["src/Layers/FastAPI.Layers.Infrastructure.Persistence/FastAPI.Layers.Infrastructure.Persistence.csproj", "src/Layers/FastAPI.Layers.Infrastructure.Persistence/"]
COPY ["src/Layers/FastAPI.Layers.Application/FastAPI.Layers.Application.csproj", "src/Layers/FastAPI.Layers.Application/"]
COPY ["src/Libraries/FastAPI.Libraries.Mapping/FastAPI.Libraries.Mapping.csproj", "src/Libraries/FastAPI.Libraries.Mapping/"]
COPY ["src/Layers/FastAPI.Layers.Domain.Common/FastAPI.Layers.Domain.Common.csproj", "src/Layers/FastAPI.Layers.Domain.Common/"]
COPY ["src/Layers/FastAPI.Layers.Domain/FastAPI.Layers.Domain.csproj", "src/Layers/FastAPI.Layers.Domain/"]
COPY ["src/Libraries/FastAPI.Libraries.System.Extensions/FastAPI.Libraries.System.Extensions.csproj", "src/Libraries/FastAPI.Libraries.System.Extensions/"]
COPY ["src/Libraries/FastAPI.Libraries.Validation/FastAPI.Libraries.Validation.csproj", "src/Libraries/FastAPI.Libraries.Validation/"]
COPY ["src/Features/Identity/FastAPI.Features.Identity.Application/FastAPI.Features.Identity.Application.csproj", "src/Features/Identity/FastAPI.Features.Identity.Application/"]
COPY ["src/Features/Identity/FastAPI.Features.Identity.Domain/FastAPI.Features.Identity.Domain.csproj", "src/Features/Identity/FastAPI.Features.Identity.Domain/"]
COPY ["src/Features/Identity/FastAPI.Features.Identity.Presentation/FastAPI.Features.Identity.Presentation.csproj", "src/Features/Identity/FastAPI.Features.Identity.Presentation/"]
COPY ["src/Layers/FastAPI.Layers.Presentation/FastAPI.Layers.Presentation.csproj", "src/Layers/FastAPI.Layers.Presentation/"]
COPY ["src/Layers/FastAPI.Layers.Configuration/FastAPI.Layers.Configuration.csproj", "src/Layers/FastAPI.Layers.Configuration/"]
COPY ["src/Layers/FastAPI.Layers.Infrastructure.Documents.Azure/FastAPI.Layers.Infrastructure.Documents.Azure.csproj", "src/Layers/FastAPI.Layers.Infrastructure.Documents.Azure/"]
COPY ["src/Layers/FastAPI.Layers.Infrastructure.Email/FastAPI.Layers.Infrastructure.Email.csproj", "src/Layers/FastAPI.Layers.Infrastructure.Email/"]
COPY ["src/Layers/FastAPI.Layers.Infrastructure.Http/FastAPI.Layers.Infrastructure.Http.csproj", "src/Layers/FastAPI.Layers.Infrastructure.Http/"]
COPY ["src/Layers/FastAPI.Layers.Infrastructure.Messaging/FastAPI.Layers.Infrastructure.Messaging.csproj", "src/Layers/FastAPI.Layers.Infrastructure.Messaging/"]
RUN dotnet restore "example/StoryBooks/FastAPI.Example.StoryBooks/FastAPI.Example.StoryBooks.csproj"
COPY . .
WORKDIR "/src/example/StoryBooks/FastAPI.Example.StoryBooks"
RUN dotnet build "FastAPI.Example.StoryBooks.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FastAPI.Example.StoryBooks.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FastAPI.Example.StoryBooks.dll"]