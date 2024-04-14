using MediatorOne.Common;

namespace MediatorOne.Features;

public class Airplane : BaseComponent
{
    public string Name { get; set; } 
    public bool IsLanding { get; set; } 
    
    private AirTrafficControlTower? _mediator;

    public Airplane(string name)
    {
        Name = name;
    }

    public void SetMediator(AirTrafficControlTower mediator)
    {
        _mediator = mediator;
    }

    public void RequestLandingClearance()
    {
        if (_mediator is null)
        {
            NotifyMediatorIsNull();
            return;
        }
        
        Console.WriteLine($"\nAirplane {Name}: Sending \"Request Landing Clearance\"");
        _mediator.Notify(this, "Request Landing Clearance");
    }
    
    public void LandingClearanceDenied()
    {
        if (_mediator is null)
        {
            NotifyMediatorIsNull();
            return;
        }
        
        Console.WriteLine($"\nAirplane {Name}: Acknowledging \"Landing Clearance Denied\".");
    }
    
    public void BeginLanding()
    {
        if (_mediator is null)
        {
            NotifyMediatorIsNull();
            return;
        }

        IsLanding = true;
        Console.WriteLine($"\nAirplane {Name}: Acknowledging \"Begin Landing\"");
    }
    
    public void CompleteLanding()
    {
        if (_mediator is null)
        {
            NotifyMediatorIsNull();
            return;
        }

        IsLanding = false;
        Console.WriteLine($"\nAirplane {Name}: Sending \"Landing Completed\"");
        _mediator.Notify(this, "Landing Completed");
    }
}