namespace FastAPI.Features.Identity.Application.Requests.Account.ConfirmEmail;

using FastAPI.Layers.Application.Request;
using FastAPI.Layers.Application.Request.Attributes;

[AppCreateRequest]
public sealed record ConfirmEmailRequest(
    string UserId,
    string Token) : AppRequest;
