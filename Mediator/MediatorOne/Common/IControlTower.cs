using MediatorOne.Features;

namespace MediatorOne.Common;

// Mediator interface: declares a method, notify, used by components to notify the mediator about requested events.
// The Mediator may react to these events and pass the execution to other components.
public interface IControlTower
{
    void Notify(Airplane sender, string eventRequest);
}