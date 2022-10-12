namespace FastAPI.Features.Identity.Application.Requests.Users.ChangePassword;

using FastAPI.Layers.Application.Request;
using FastAPI.Layers.Application.Request.Attributes;

[AppUpdateRequest]
public sealed record ChangePasswordRequest() : AppRequest;