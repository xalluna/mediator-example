using MediatorTwo.Common;
using MediatorTwo.Features;

var mediator = new Mediator();
mediator.RegisterHandler(typeof(PrintRequest), new PrintRequestHandler());
var outMessage = mediator.Handle(new PrintRequest("we mediate"));
Console.WriteLine(outMessage);

