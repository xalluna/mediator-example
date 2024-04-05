using MediatorTwo.Common;

namespace MediatorTwo.Features;

public class Airplane
{
    private readonly AirTrafficControlTower _mediator;
    public string Name { get; set; }
    public Airplane(AirTrafficControlTower mediator)
    {
        _mediator = mediator;
    }
    public void RequestTakeoff() {
        _mediator.RequestTakeoff(this);
    }
    
    public void RequestLanding() {
        _mediator.RequestLanding(this);
    }
    
    public void NotifyAirTrafficControl(string message) {
        Console.WriteLine($"Airplane {Name}: {message}");
    }
}