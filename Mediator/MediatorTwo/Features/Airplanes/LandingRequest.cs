using MediatorTwo.Common;

namespace MediatorTwo.Features.Airplanes;

public class LandingRequest: IRequest
{
}

public class LandingRequestHandler : IRequestHandler<LandingRequest>
{
    private readonly Airplane _sender;
    private readonly IControlTower _controlTower;

    public LandingRequestHandler(
        Airplane sender,
        IControlTower controlTower)
    {
        _sender = sender;
        _controlTower = controlTower;
    }
    
    public void Handle(LandingRequest request)
    {
        Logger.Log("Landing Request", $"Checking if {_sender.Flight} can land.");
        
        if (_sender.IsTakingOff)
        {
            Logger.Log("Landing Clearance Denied", $"{_sender.Flight} is taking off.", LogLevel.Error);
            return;
        }
        
        if (_sender.IsLanding)
        {
            Logger.Log("Landing Clearance Denied", $"{_sender.Flight} is already landing.", LogLevel.Error);
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
            Logger.Log("Landing Clearance Denied", $"{activeAircraft.Flight} on runway.", LogLevel.Warn);

            return;
        }

        Logger.Log("Landing Clearance Approved", $"{_sender.Flight} has entered runway for landing.", LogLevel.Success);
        _sender.IsLanding = true;
    }
}