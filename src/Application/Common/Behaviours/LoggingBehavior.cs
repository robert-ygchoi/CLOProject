using MediatR.Pipeline;
using Microsoft.Extensions.Logging;

namespace Application.Common.Behaviours;

public class RequestLoggingBehavior<TRequest> : IRequestPreProcessor<TRequest> where TRequest : notnull
{
    private readonly ILogger _logger;

    public RequestLoggingBehavior(ILogger<TRequest> logger)
    {
        _logger = logger;

    }

    public Task Process(TRequest request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Request: {@Request}", request);

        return Task.CompletedTask;
    }
}

public class ResponseLoggingBehavior<TRequest, TResponse> : IRequestPostProcessor<TRequest, TResponse> where TRequest : notnull
{
    private readonly ILogger _logger;

    public ResponseLoggingBehavior(ILogger<TRequest> logger)
    {
        _logger = logger;
        
    }

    public Task Process(TRequest request, TResponse response, CancellationToken cancellationToken)
    {
        // TODO: response model message 개선
        _logger.LogInformation("Response: {@Response}", response);

        return Task.CompletedTask;
    }
}