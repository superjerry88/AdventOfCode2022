var lines = File.ReadAllLines("input.txt");
var calories = new List<int> {0};

foreach (var line in lines)
{
    if (line == "")
    {
        calories.Add(0);
    }
    else
    {
        calories[^1] += int.Parse(line);
    }
}

Console.WriteLine($"Part 1: {calories.Max()}");
Console.WriteLine($"Part 2: {calories.OrderByDescending(c => c).Take(3).Sum()}");