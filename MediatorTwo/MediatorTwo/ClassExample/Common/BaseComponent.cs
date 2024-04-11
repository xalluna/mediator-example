namespace MediatorTwo.ClassExample.Common;

// Base Component stores a mediator's instance inside component objects, also allows to set mediator for component
public class BaseComponent
{
    protected IControlTower ControlTower;

    protected BaseComponent(IControlTower controlTower = null!)
    {
        ControlTower = controlTower;
    }

    public void SetMediator(IControlTower controlTower)
    {
        ControlTower = controlTower;
    }

    protected static void NotifyMediatorIsNull()
    {
        Console.WriteLine("Mediator not set");
    }
}