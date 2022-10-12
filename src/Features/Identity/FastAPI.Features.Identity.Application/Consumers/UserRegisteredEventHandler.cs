namespace FastAPI.Features.Identity.Application.Consumers;

using FastAPI.Features.Identity.Application.Services;
using FastAPI.Features.Identity.Domain.Events;
using FastAPI.Features.Identity.Domain.Repositories;
using FastAPI.Features.Identity.Domain.Services.Users;
using FastAPI.Layers.Application;
using FastAPI.Layers.Domain.Events.Abstractions;

using System.Threading;
using System.Threading.Tasks;

public sealed class UserRegisteredEventHandler : IDomainEventHandler<UserCreatedEvent>
{
    private readonly IUserRepository userRepository;
    private readonly IIdentityEmailService emailService;
    private readonly IUserManager userManager;
    private readonly IHttpUtilities httpUtilities;

    /// <summary>
    /// Initializes a new instance of the <see cref="UserRegisteredEventHandler"/> class.
    /// </summary>
    /// <param name="userRepository">User repository.</param>
    /// <param name="emailService">Identity email service.</param>
    /// <param name="userManager">User manager.</param>
    /// <param name="httpUtilities">HTTP utilities.</param>
    public UserRegisteredEventHandler(
        IUserRepository userRepository,
        IIdentityEmailService emailService,
        IUserManager userManager,
        IHttpUtilities httpUtilities)
    {
        this.userRepository = userRepository;
        this.emailService = emailService;
        this.userManager = userManager;
        this.httpUtilities = httpUtilities;
    }

    public async Task Handle(UserCreatedEvent notification, CancellationToken cancellationToken)
    {
        var user = await this.userRepository.GetAsync(notification.Id, cancellationToken);
        if (user is null)
        {
            return;
        }

        string? token = await this.userManager.GenerateEmailConfirmationTokenAsync(user);
        string? urlSafeToken = this.httpUtilities.UrlEncode(token);
        if (urlSafeToken is not null)
        {
            await this.emailService.SendUserRegisteredEmail(user, urlSafeToken, cancellationToken);
        }
    }
}
