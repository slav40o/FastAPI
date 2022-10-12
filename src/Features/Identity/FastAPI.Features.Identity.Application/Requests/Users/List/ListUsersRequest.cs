namespace FastAPI.Features.Identity.Application.Requests.Users.List;

using FastAPI.Layers.Application.Request;

public sealed record ListUsersRequest : AppQueryRequest<UserListItem>;
