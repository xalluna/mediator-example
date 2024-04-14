namespace MediatorTwo.Features.Carriers;

public static class Carrier
{
    public static readonly Dictionary<string, string> Map = new()
    {
        {"American Airlines", "AA"},
        {"Delta Airlines", "DL"},
        {"Southwest Airlines", "WN"}
    };

    public static List<string> Carriers => Map.Select(x => x.Key).ToList();

    private static readonly Random Rng = new ();
    public static string Random()
    {
        return Carriers[Rng.Next(0, Carriers.Count)];
    }
}