using MediatorTwo.Common;

namespace MediatorTwo.Features.Airplanes;

public class CompleteLandingRequest: IRequest
{
}

public class CompleteLandingRequestHandler : IRequestHandler<CompleteLandingRequest>
{
    private readonly Airplane _sender;
    private readonly IControlTower _controlTower;

    public CompleteLandingRequestHandler(
        Airplane sender,
        IControlTower controlTower)
    {
        _sender = sender;
        _controlTower = controlTower;
    }
    
    public void Handle(CompleteLandingRequest request)
    {
        if (!_sender.IsLanding)
        {
            Logger.Log("Complete Landing Error", $"{_sender.Flight} is not landing.", LogLevel.Error);
            return;
        }
        
        Logger.Log("Complete Landing", $"{_sender.Flight} has landed and left runway.", LogLevel.Success);
        _sender.IsLanding = false;
    }
}