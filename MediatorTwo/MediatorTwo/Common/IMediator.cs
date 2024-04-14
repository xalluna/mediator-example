namespace MediatorTwo.Common;

public interface IRequest {}
public interface IRequest<TResponse> {}

public interface IRequestHandler<in TRequest>
    where TRequest: IRequest
{
    public void Handle(TRequest request);
}

public interface IRequestHandler<in TRequest, out TResponse>
where TRequest: IRequest<TResponse>
{
    public TResponse Handle(TRequest request);
}

public interface IMediator
{
    public void RegisterHandler(Type requestType, Type handler);
    public void Notify(IRequest request);
    public TResponse Notify<TResponse>(IRequest<TResponse> request);
}

public interface IMediator<in T>: IMediator
{
    public void Notify(T sender, IRequest request);
    public TResponse Notify<TResponse>(T sender, IRequest<TResponse> request);
}