namespace MediatorTwo.Common;

public class Mediator: IMediator
{
    private readonly Dictionary<Type, Func<object>> Handlers = new ();

    public void RegisterHandler(Type requestType, Type handlerType)
    {
        var handle = handlerType.GetMethod(IRequestHandler.Handle);

        if (handle is null)
        {
            throw new ArgumentException($"{handlerType.Name} does not have {IRequestHandler.Handle} method");
        }

        Handlers.Add(requestType, () => Activator.CreateInstance(handlerType)!);
    }

    public TResponse Notify<TResponse>(IRequest<TResponse> request)
    {
        var hasKey = Handlers.TryGetValue(request.GetType(), out var handlerConstructor);

        if (!hasKey || handlerConstructor is null)
        {
            throw new ApplicationException($"No valid handler registered for {request.GetType().Name}");
        }

        var handler = handlerConstructor.Invoke();

        return (TResponse) handler.GetType().GetMethod(IRequestHandler.Handle)!.Invoke(handlerConstructor, new []{ request });
    }
}