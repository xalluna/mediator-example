using MediatorTwo.ClassExample.Features;
namespace MediatorTwo.ClassExample.Common;

// Concrete mediator: implements cooperative behavior by coordinating several components through the notify method from the interface.
public class AirTrafficControlTower : IControlTower
{
    private readonly Airplane _airplane1;
    private readonly Airplane _airplane2;

    // Constructor sets mediator through setMediator method from BaseComponent
    public AirTrafficControlTower(Airplane airplane1, Airplane airplane2)
    {
        _airplane1 = airplane1;
        _airplane1.SetMediator(this);
        _airplane2 = airplane2;
        _airplane2.SetMediator(this);
    } 
    
    // Handles event requests
    public void Notify(Airplane sender, string eventRequest)
    {
        switch (eventRequest)
        {
            case "Request Landing Clearance":
                Console.WriteLine("Mediator: Reacts on \"Request Landing Clearance\" and checks if Airplane can land:");
                
                var landingAirplane = _airplane1.IsLanding ? _airplane1 : _airplane1.IsLanding ? _airplane2 : null;
                
                if (landingAirplane is not null)
                {
                    Console.WriteLine($"Mediator: Sends \"Landing Clearance Denied\". {landingAirplane.Name}'s Landing is in progress.");
                    sender.LandingClearanceDenied();
                    break;
                }
                
                Console.WriteLine($"Mediator: Sends \"Begin Landing\" to {sender.Name}:");
                sender.BeginLanding();
                break;

            case "Landing Completed":
                Console.WriteLine("Mediator: Reacts on \"Landing Completed\" and no further actions at this time.");
                break;
            
            default:
                Console.WriteLine("Request Not Recognized.");
                break;
        }
    }
}