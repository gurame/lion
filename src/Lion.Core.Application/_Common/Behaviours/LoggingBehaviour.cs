using Lion.Core.Application._Common.Interfaces;
using MediatR.Pipeline;
using Microsoft.Extensions.Logging;

namespace Lion.Core.Application._Common.Behaviours;

public class LoggingBehaviour<TRequest> : IRequestPreProcessor<TRequest> where TRequest : notnull
{
    private readonly ILogger _logger;
    private readonly IIdentityService _identityService;

    public LoggingBehaviour(ILogger<TRequest> logger, IIdentityService identityService)
    {
        _logger = logger;
        _identityService = identityService;
    }

    public Task Process(TRequest request, CancellationToken cancellationToken)
    {
        var requestName = typeof(TRequest).Name;
        var userId = _identityService.GetUserId();
        string userName = _identityService.GetUserName();

        _logger.LogInformation("Lion Request: {Name} {@UserId} {@UserName} {@Request}",
                                requestName, userId, userName, request);

        return Task.CompletedTask;
    }
}