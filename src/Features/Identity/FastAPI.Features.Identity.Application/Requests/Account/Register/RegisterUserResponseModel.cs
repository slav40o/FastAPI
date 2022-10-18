namespace FastAPI.Features.Identity.Application.Requests.Account.Register;

using FastAPI.Features.Identity.Domain.Services.Auth;

public sealed record RegisterUserResponseModel(string UserId, LoginDataModel? LoginData);