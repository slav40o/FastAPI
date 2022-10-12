namespace RequestTemplate.Tests;

using FastAPI.Layers.Application.Request;
using FastAPI.Layers.Application.Request.Attributes;

[AppGetRequest]
public sealed record GetRequest() : AppRequest;