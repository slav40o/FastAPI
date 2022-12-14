namespace FastAPI.Features.Identity.Application.Requests.Account.UpdateDetails;

using FastAPI.Features.Identity.Domain.Entities;
using FastAPI.Layers.Application.Request;
using FastAPI.Layers.Application.Request.Attributes;

[AppUpdateRequest]
[AppAuthorize(RequestPolicies.ConfirmedEmailOnly)]
public sealed record UpdateDetailsRequest(
    string Id,
    string FirstName,
    string LastName,
    string? PhoneNumber,
    Gender? Gender,
    DateTime? DateOfBirth) : AppRequest;