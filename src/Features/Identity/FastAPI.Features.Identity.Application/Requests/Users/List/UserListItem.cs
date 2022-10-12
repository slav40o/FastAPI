namespace FastAPI.Features.Identity.Application.Requests.Users.List;

public sealed record UserListItem(string Id, string Name, string Email);