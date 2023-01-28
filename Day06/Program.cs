var line = File.ReadAllText("input.txt");

Console.WriteLine($"Part1: {FindMarker(line,4)}");
Console.WriteLine($"Part1: {FindMarker(line,14)}");

static int FindMarker(string input, int packetSize)
{
    for (var i = 0; i < input.Length - packetSize; i++)
    {
        if (input.Skip(i).Take(packetSize).Distinct().Count() != packetSize) continue;
        return i + packetSize;
    }
    return 0;
}