namespace SpaceTrading.Production.Api.Middleware
{
    public class CorrelationIdMiddleware
    {
        private const string CorrelationIdHeaderKey = "X-Correlation-ID";
        private readonly RequestDelegate _next;


        public CorrelationIdMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            // Decorate the request
            if (!context.Request.Headers.TryGetValue(CorrelationIdHeaderKey, out var correlationId))
            {
                correlationId = Guid.NewGuid().ToString();
                context.Request.Headers.Add(CorrelationIdHeaderKey, correlationId);
            }

            // Decorate the response
            context.Response.OnStarting(() =>
            {
                context.Response.Headers.Add(CorrelationIdHeaderKey, correlationId);
                return Task.CompletedTask;
            });

            await _next(context);
        }
    }
}