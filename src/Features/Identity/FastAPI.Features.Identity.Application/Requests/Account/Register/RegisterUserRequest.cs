namespace FastAPI.Features.Identity.Application.Requests.Account.Register;

using FastAPI.Layers.Application.Request;
using FastAPI.Layers.Application.Request.Attributes;

[AppCreateRequest]
public sealed record RegisterUserRequest(
    string FirstName,
    string LastName,
    string Email,
    string Password,
    string ConfirmPassword) : AppRequest<RegisterUserResponseModel>;