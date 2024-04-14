using MediatorTwo.Common;

namespace MediatorTwo.Features.Maintenance;

public class RepairRunwayRequest: IRequest
{
}

public class RepairRunwayRequestHandler : IRequestHandler<RepairRunwayRequest>
{
    private readonly IControlTower _controlTower;

    public RepairRunwayRequestHandler(IControlTower controlTower)
    {
        _controlTower = controlTower;
    }
    
    public void Handle(RepairRunwayRequest request)
    {
        if (_controlTower.IsRunwayUnderMaintenance)
        {
            Logger.Log("Maintenance Error", "Runway is already under maintenance.", LogLevel.Error);
            return;
        }
        
        if (!_controlTower.IsRunwayClear)
        {
            Logger.Log("Maintenance Error", "Runway is in use.", LogLevel.Error);
            return;
        }
        
        Logger.Log("Maintenance Request", "Runway maintenance has started.", LogLevel.Success);
        _controlTower.IsRunwayUnderMaintenance = true;
    }
}