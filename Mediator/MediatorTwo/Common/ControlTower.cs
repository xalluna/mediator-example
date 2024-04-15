using System.Reflection;
using MediatorTwo.Features.Airplanes;

namespace MediatorTwo.Common;

public interface IControlTower : IMediator<Airplane>
{
    public List<Airplane> Airplanes { get; }
    public Airplane AircraftOnRunway { get; }
    public bool IsRunwayClear { get; }
    public bool IsRunwayUnderMaintenance { get; set; }
    
    public int GetNextFlightNumber();
}

public class ControlTower: IControlTower
{
    protected readonly Dictionary<Type, Type> Handlers = new();
    public List<Airplane> Airplanes { get; } = new();
    public Airplane AircraftOnRunway => Airplanes.FirstOrDefault(x => x.IsTakingOff || x.IsLanding);
    public bool IsRunwayClear => !Airplanes.Any(x => x.IsTakingOff || x.IsLanding);
    public bool IsRunwayUnderMaintenance { get; set; }

    private static int _nextFlightNumber = 1;
    
    public ControlTower()
    {
        var assembly = Assembly.GetExecutingAssembly();

        var handlers = assembly.GetTypes()
            .Where(t => t.GetInterfaces().Any(i => i.IsGenericType &&
               (i.GetGenericTypeDefinition() == typeof(IRequestHandler<>) ||
                i.GetGenericTypeDefinition() == typeof(IRequestHandler<,>))))
            .ToList();

        foreach (var handler in handlers)
        {
            var genericHandler = handler.GetInterfaces().FirstOrDefault(i => i.IsGenericType &&
                 (i.GetGenericTypeDefinition() == typeof(IRequestHandler<>) ||
                  i.GetGenericTypeDefinition() == typeof(IRequestHandler<,>)));
            
            if (genericHandler is null) continue;
            
            RegisterHandler(genericHandler.GenericTypeArguments[0], handler);
        }
    }

    public void RegisterHandler(Type requestType, Type handlerType)
    {
        var handle = handlerType.GetMethod("Handle");

        if (handle is null)
        {
            throw new ArgumentException($"{handlerType.Name} does not have Handle method");
        }

        Handlers.Add(requestType, handlerType);
    }

    private object GetHandler(Type requestType, params object[] dependencies)
    {
        var hasKey = Handlers.TryGetValue(requestType, out var handlerType);

        if (!hasKey || handlerType is null)
        {
            throw new ApplicationException($"No valid handler registered for {requestType.Name}");
        }
        
        var parameters = handlerType.GetConstructors()[0].GetParameters();

        if (parameters.Length > dependencies.Length)
        {
            throw new ArgumentException("Not enough parameters for constructor");
        }

        var foo = new List<object>();

        foreach (var parameter in parameters)
        {
            var dependency = dependencies.FirstOrDefault(x =>
                x.GetType() == parameter.ParameterType || x.GetType().GetInterfaces().Contains(parameter.ParameterType));

            if (dependency is not null)
            {
                foo.Add(dependency);
            }
        }
        
        var handler = Activator.CreateInstance(handlerType, foo.ToArray())!;

        return handler;
    }

    private static object ExecuteHandle(object handler, IRequest request)
    {
        return handler.GetType().GetMethod("Handle")!.Invoke(handler, new []{ request });
    }
    
    public void Notify(IRequest request)
    {
        var handler = GetHandler(request.GetType(), this);
        ExecuteHandle(handler, request);
    }

    public TResponse Notify<TResponse>(IRequest<TResponse> request)
    {
        var handler = GetHandler(request.GetType(), this);
        return (TResponse) ExecuteHandle(handler, request);
    }

    public void Notify(Airplane sender, IRequest request)
    {
        var handler = GetHandler(request.GetType(), this, sender);
        ExecuteHandle(handler, request);
    }

    public TResponse Notify<TResponse>(Airplane sender, IRequest<TResponse> request)
    {
        var handler = GetHandler(request.GetType(), this, sender);
        return (TResponse) ExecuteHandle(handler, request);
    }

    public int GetNextFlightNumber()
    {
        var flightNumber = _nextFlightNumber;
        _nextFlightNumber++;
        
        return flightNumber;
    }
}