using MediatorTwo.Common;
using MediatorTwo.Features.Carriers;

namespace MediatorTwo.Features.Airplanes;

public interface IAirplane
{
    public string CarrierName { get; set; }
    public int FlightNumber { get; set; }
    public bool IsTakingOff { get; set; }
    public bool IsLanding { get; set; }

    public void RequestTakeoff();
    public void CompleteTakeoff();
    public void RequestLanding();
    public void CompleteLanding();
}

public class Airplane: IAirplane
{
    private readonly IControlTower _controlTower;
    
    public string CarrierName { get; set; }
    public int FlightNumber { get; set; }
    public bool IsTakingOff { get; set; }
    public bool IsLanding { get; set; }

    public string Flight => $"{Carrier.Map[CarrierName]}{FlightNumber}";
    
    public Airplane(IControlTower controlTower)
    {
        CarrierName = Carrier.Random();
        FlightNumber = controlTower.GetNextFlightNumber();
        
        controlTower.Airplanes.Add(this);
        _controlTower = controlTower;
        
        Logger.Log("Airplane Registration", $"{Flight} has registered with Control Tower.");
    }

    public void RequestTakeoff() => _controlTower.Notify(this, new TakeoffRequest());
    public void CompleteTakeoff() => _controlTower.Notify(this, new CompleteTakeoffRequest());
    public void RequestLanding() => _controlTower.Notify(this, new LandingRequest());
    public void CompleteLanding() => _controlTower.Notify(this, new CompleteLandingRequest());
}