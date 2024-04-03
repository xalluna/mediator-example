using MediatorTwo.Common;

namespace MediatorTwo.Features;

public class PrintRequest : IRequest<string>
{
    public string Message { get; set; }

    public PrintRequest(string message)
    {
        Message = message;
    }
}

public class PrintRequestHandler : IRequestHandler<PrintRequest, string>
{
    public string Handle(PrintRequest request)
    {
        Console.WriteLine($"Message from request: {request.Message}");

        return "we make it 😅";
    }
}

public class BrokenHandler : IRequestHandler
{
}