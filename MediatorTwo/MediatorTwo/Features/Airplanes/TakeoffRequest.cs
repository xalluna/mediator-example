using MediatorTwo.Common;

namespace MediatorTwo.Features.Airplanes;

public class TakeoffRequest: IRequest<bool>
{
    public Airplane Sender { get; set; }
}