using MediatorTwo.Features.Airplanes;
using MediatorTwo.Features.Maintenance;

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
        RegisterHandler(typeof(TakeoffRequest), typeof(TakeoffRequestHandler));
        RegisterHandler(typeof(CompleteTakeoffRequest), typeof(CompleteTakeoffRequestHandler));
        RegisterHandler(typeof(LandingRequest), typeof(LandingRequestHandler));
        RegisterHandler(typeof(CompleteLandingRequest), typeof(CompleteLandingRequestHandler));
        RegisterHandler(typeof(RepairRunwayRequest), typeof(RepairRunwayRequestHandler));
        RegisterHandler(typeof(CompleteMaintenanceRequest), typeof(CompleteMaintenanceRequestHandler));
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
    
    public void Notify(IRequest request)
    {
        var hasKey = Handlers.TryGetValue(request.GetType(), out var handlerType);

        if (!hasKey || handlerType is null)
        {
            throw new ApplicationException($"No valid handler registered for {request.GetType().Name}");
        }

        var handler = Activator.CreateInstance(handlerType, this)!;

        handler.GetType().GetMethod("Handle")!.Invoke(handler, new []{ request });
    }

    public TResponse Notify<TResponse>(IRequest<TResponse> request)
    {
        var hasKey = Handlers.TryGetValue(request.GetType(), out var handlerType);

        if (!hasKey || handlerType is null)
        {
            throw new ApplicationException($"No valid handler registered for {request.GetType().Name}");
        }

        var handler = Activator.CreateInstance(handlerType, this)!;

        return (TResponse) handler.GetType().GetMethod("Handle")!.Invoke(handler, new []{ request });
    }

    public void Notify(Airplane sender, IRequest request)
    {
        var hasKey = Handlers.TryGetValue(request.GetType(), out var handlerType);

        if (!hasKey || handlerType is null)
        {
            throw new ApplicationException($"No valid handler registered for {request.GetType().Name}");
        }

        var handler = Activator.CreateInstance(handlerType, sender, this)!;

        handler.GetType().GetMethod("Handle")!.Invoke(handler, new []{ request });
    }

    public TResponse Notify<TResponse>(Airplane sender, IRequest<TResponse> request)
    {
        var hasKey = Handlers.TryGetValue(request.GetType(), out var handlerType);

        if (!hasKey || handlerType is null)
        {
            throw new ApplicationException($"No valid handler registered for {request.GetType().Name}");
        }

        var handler = Activator.CreateInstance(handlerType, sender, this)!;

        return (TResponse) handler.GetType().GetMethod("Handle")!.Invoke(handler, new []{ request });
    }
    
    public int GetNextFlightNumber()
    {
        var flightNumber = _nextFlightNumber;
        _nextFlightNumber++;
        
        return flightNumber;
    }
}