using MediatorTwo.Common;
using MediatorTwo.Features;

var mediator = new Mediator();
mediator.RegisterHandler(typeof(PrintRequest), new PrintRequestHandler());
var outMessage = mediator.Handle(new PrintRequest("we mediate"));
Console.WriteLine(outMessage);


//Air Control Example
var controlTower = new AirTrafficControlTower();
 
var airplane1 = new Airplane(controlTower)
{
    Name = "Barbie"
};

var airplane2 = new Airplane(controlTower)
{
    Name = "Ken"
};

airplane1.RequestTakeoff();
airplane2.RequestLanding();