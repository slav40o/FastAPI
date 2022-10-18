namespace FastAPI.Features.Identity.Application.Requests.Account.List;

public sealed record UserListItem(string Id, string Name, string Email);