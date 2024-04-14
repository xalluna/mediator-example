using MediatorTwo.Common;
using MediatorTwo.Features;

var mediator = new Mediator();
mediator.RegisterHandler(typeof(PrintRequest), typeof(PrintRequestHandler));
var outMessage = mediator.Notify(new PrintRequest("we mediate"));
Console.WriteLine(outMessage);
