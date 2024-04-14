using MediatorTwo.Common;

namespace MediatorTwo.Features.Airplanes;

public class TakeoffRequest: IRequest
{
}

public class TakeoffRequestHandler : IRequestHandler<TakeoffRequest>
{
    private readonly Airplane _sender;
    private readonly IControlTower _controlTower;

    public TakeoffRequestHandler(
        Airplane sender,
        IControlTower controlTower)
    {
        _sender = sender;
        _controlTower = controlTower;
    }
    
    public void Handle(TakeoffRequest request)
    {
        Logger.Log("Takeoff Request", $"Checking if {_sender.Flight} can takeoff.");
        
        if (_sender.IsLanding)
        {
            Logger.Log("Takeoff Clearance Denied", $"{_sender.Flight} is landing.", LogLevel.Error);
            return;
        }
        
        if (_sender.IsTakingOff)
        {
            Logger.Log("Takeoff Clearance Denied", $"{_sender.Flight} is already taking off.", LogLevel.Error);
            return;
        }
        
        if (_controlTower.IsRunwayUnderMaintenance)
        {
            Logger.Log("Takeoff Clearance Denied", "Runway is under maintenance.", LogLevel.Warn);
            return;
        }
                
        if (!_controlTower.IsRunwayClear)
        {
            var activeAircraft = _controlTower.AircraftOnRunway;
            Logger.Log("Takeoff Clearance Denied", $"{activeAircraft.Flight} on runway.", LogLevel.Warn);

            return;
        }

        Logger.Log("Takeoff Clearance Approved", $"{_sender.Flight} has entered runway for takeoff.", LogLevel.Success);
        _sender.IsTakingOff = true;
    }
}