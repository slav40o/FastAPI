namespace FastAPI.Features.Identity.Application.Requests.Users.List;

using FastAPI.Features.Identity.Domain.Repositories;
using FastAPI.Layers.Application.Handlers;

internal sealed class ListUsersRequestHandler : AppQueryRequestHandler<ListUsersRequest, UserListItem>
{
    private readonly IUserRepository userRepository;

    public ListUsersRequestHandler(IUserRepository userRepository)
    {
        this.userRepository = userRepository;
    }

    public override Task<IQueryable<UserListItem>> HandleRequest(ListUsersRequest request, CancellationToken cancellationToken)
    {
        var query = userRepository
            .All()
            .Select(u => new UserListItem(u.Id, u.FirstName + " " + u.LastName, u.Email));

        return Task.FromResult(query);
    }
}
