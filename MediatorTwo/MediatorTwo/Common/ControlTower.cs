using MediatorTwo.Features.Airplanes;

namespace MediatorTwo.Common;

public class ControlTower: Mediator
{
    public List<Airplane> Airplanes { get; set; }
    
    public ControlTower()
    {
    }
    
    
}