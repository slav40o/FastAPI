namespace FastAPI.Features.Identity.Application.Requests.Account.ChangePassword;

using FastAPI.Layers.Application.Request;
using FastAPI.Layers.Application.Request.Attributes;

[AppUpdateRequest]
public sealed record ChangePasswordRequest(
    string UserId,
    string OldPassword,
    string NewPassword,
    string ConfirmNewPassword) : AppRequest;