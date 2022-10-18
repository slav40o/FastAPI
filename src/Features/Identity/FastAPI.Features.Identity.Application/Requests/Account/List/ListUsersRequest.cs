namespace FastAPI.Features.Identity.Application.Requests.Account.List;

using FastAPI.Layers.Application.Request;

public sealed record ListUsersRequest : AppQueryRequest<UserListItem>;
