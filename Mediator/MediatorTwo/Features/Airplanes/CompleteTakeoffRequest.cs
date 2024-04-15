using MediatorTwo.Common;

namespace MediatorTwo.Features.Airplanes;

public class CompleteTakeoffRequest: IRequest
{
}

public class CompleteTakeoffRequestHandler : IRequestHandler<CompleteTakeoffRequest>
{
    private readonly Airplane _sender;
    private readonly IControlTower _controlTower;

    public CompleteTakeoffRequestHandler(
        Airplane sender,
        IControlTower controlTower)
    {
        _sender = sender;
        _controlTower = controlTower;
    }
    
    public void Handle(CompleteTakeoffRequest request)
    {
        if (!_sender.IsTakingOff)
        {
            Logger.Log("Complete Takeoff Error", $"{_sender.Flight} is not taking off.", LogLevel.Error);
            return;
        }
        
        Logger.Log("Complete Takeoff", $"{_sender.Flight} has taken off and left runway.", LogLevel.Success);
        _sender.IsTakingOff = false;
    }
}