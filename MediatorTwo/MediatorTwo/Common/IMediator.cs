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

public interface IMediator
{
    public void RegisterHandler(Type requestType, Type handler);
    public TResponse Notify<TResponse>(IRequest<TResponse> request);
}