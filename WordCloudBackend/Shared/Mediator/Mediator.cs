namespace WordCloudBackend.Shared.Mediator;

public class Mediator : IMediator
{
    private readonly IServiceProvider _serviceProvider;

    public Mediator(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task<TResponse> Send<TResponse>(IRequest<TResponse> request)
    {
        var requestType = request.GetType();
        var handlerType = typeof(IRequestHandler<,>).MakeGenericType(requestType, typeof(TResponse));
        
        var handler = _serviceProvider.GetService(handlerType);

        if (handler == null)
        {
            throw new InvalidOperationException($"No handler registered for {requestType.Name}");
        }

        var method = handlerType.GetMethod("Handle");
        if (method == null)
        {
             throw new InvalidOperationException($"Handle method not found on {handlerType.Name}");
        }

        var task = (Task<TResponse>)method.Invoke(handler, [request])!;
        if (task == null)
        {
            throw new InvalidOperationException($"Handle method returned null for {handlerType.Name}");
        }

        return await task;
    }
}