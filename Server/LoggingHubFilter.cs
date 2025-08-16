using Microsoft.AspNetCore.SignalR;

namespace Server {
    public class LoggingHubFilter : IHubFilter {
        private readonly ILogger<LoggingHubFilter> _logger;

        public LoggingHubFilter(ILogger<LoggingHubFilter> logger) {
            _logger = logger;
        }

        public async ValueTask<object?> InvokeMethodAsync(
            HubInvocationContext invocationContext,
            Func<HubInvocationContext, ValueTask<object?>> next) {
            try {
                return await next(invocationContext);
            } catch (Exception ex) {
                _logger.LogError(ex, "An exception occurred while invoking method {MethodName} on hub {HubName}.",
                    invocationContext.HubMethodName, invocationContext.Hub.GetType().Name);
                throw;
            }
        }
    }
}
