namespace MediatorTwo.Common;

public interface IRequest<TResponse> {}

public interface IRequestHandler
{
    public const string Handle = nameof(Handle);
}

public interface IRequestHandler<in TRequest, out TResponse>: IRequestHandler
where TRequest: IRequest<TResponse>
{
    public TResponse Handle(TRequest request);
}

public class Mediator
{
    private readonly Dictionary<Type, IRequestHandler> Handlers = new ();

    public void RegisterHandler(Type requestType, IRequestHandler handler)
    {
        var handle = handler.GetType().GetMethod(IRequestHandler.Handle);

        if (handle is null)
        {
            throw new ArgumentException($"{handler.GetType().Name} does not have {IRequestHandler.Handle} method");
        }
        
        Handlers.Add(requestType, handler);
    }

    public TResponse Handle<TResponse>(IRequest<TResponse> request)
    {
        var hasKey = Handlers.TryGetValue(request.GetType(), out var handler);

        if (!hasKey || handler is null)
        {
            throw new ApplicationException($"No valid handler registered for {request.GetType().Name}");
        }

        return (TResponse) handler.GetType().GetMethod(IRequestHandler.Handle)!.Invoke(handler, new []{ request });
    }
}