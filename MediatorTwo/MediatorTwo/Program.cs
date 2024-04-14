using MediatorTwo.Common;
using MediatorTwo.Features.Airplanes;
using MediatorTwo.Features.Maintenance;

var controlTower = new ControlTower();

var airplane1 = new Airplane(controlTower);
var airplane2 = new Airplane(controlTower);
var airplane3 = new Airplane(controlTower);
Console.WriteLine();

airplane1.RequestTakeoff();
airplane2.RequestLanding();
Console.WriteLine();

airplane1.CompleteTakeoff();
airplane2.RequestLanding();
Console.WriteLine();

airplane2.RequestLanding();
airplane2.CompleteLanding();
Console.WriteLine();

airplane3.RequestLanding();
controlTower.Notify(new RepairRunwayRequest());
Console.WriteLine();

airplane3.CompleteLanding();
controlTower.Notify(new RepairRunwayRequest());
Console.WriteLine();

airplane1.RequestLanding();
controlTower.Notify(new CompleteMaintenanceRequest());
Console.WriteLine();

airplane1.RequestLanding();
airplane1.CompleteLanding();
