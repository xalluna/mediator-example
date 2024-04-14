using MediatorTwo.Common;

namespace MediatorTwo.Features.Maintenance;

public class CompleteMaintenanceRequest: IRequest
{
}

public class CompleteMaintenanceRequestHandler : IRequestHandler<CompleteMaintenanceRequest>
{
    private readonly IControlTower _controlTower;

    public CompleteMaintenanceRequestHandler(IControlTower controlTower)
    {
        _controlTower = controlTower;
    }
    
    public void Handle(CompleteMaintenanceRequest request)
    {
        if (!_controlTower.IsRunwayUnderMaintenance)
        {
            Logger.Log("Maintenance Error", "Runway is not under maintenance.", LogLevel.Error);
            return;
        }
        
        if (!_controlTower.IsRunwayClear)
        {
            Logger.Log("Maintenance Error", "Runway is in use.", LogLevel.Error);
            return;
        }
        
        Logger.Log("Maintenance Request", "Runway maintenance has completed.", LogLevel.Success);
        _controlTower.IsRunwayUnderMaintenance = false;
    }
}