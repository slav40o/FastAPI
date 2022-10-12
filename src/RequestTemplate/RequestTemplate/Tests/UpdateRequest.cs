namespace RequestTemplate.Tests;

using FastAPI.Layers.Application.Request;
using FastAPI.Layers.Application.Request.Attributes;

[AppUpdateRequest]
public sealed record UpdateRequest() : AppRequest;