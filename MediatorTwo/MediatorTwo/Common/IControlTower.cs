using MediatorTwo.Features;

namespace MediatorTwo.Common;

public interface IControlTower
{
    void RequestTakeoff(Airplane airplane);
    void RequestLanding(Airplane airplane);
}

public class AirTrafficControlTower : IControlTower
{
    public void RequestTakeoff(Airplane airplane) 
    {
        airplane.NotifyAirTrafficControl("Requesting takeoff clearance.");
    }
 
    public void RequestLanding(Airplane airplane) 
    {
        airplane.NotifyAirTrafficControl("Requesting landing clearance.");
    }
}