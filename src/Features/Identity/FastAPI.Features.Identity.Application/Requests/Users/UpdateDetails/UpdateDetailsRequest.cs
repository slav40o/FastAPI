namespace FastAPI.Features.Identity.Application.Requests.Users.UpdateDetails;

using FastAPI.Features.Identity.Domain.Entities;
using FastAPI.Layers.Application.Request;
using FastAPI.Layers.Application.Request.Attributes;

[AppAuthorize]
[AppUpdateRequest]
public sealed record UpdateDetailsRequest(
    string Id,
    string FirstName,
    string LastName,
    string? PhoneNumber,
    Gender? Gender,
    DateTime? DateOfBirth) : AppRequest;