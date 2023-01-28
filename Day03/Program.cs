var lines = File.ReadAllLines("input.txt");

var counter = 0;
foreach (var line in lines)
{
    var sack1 = line.Take(line.Length/2).ToList();
    var sack2 = line.Skip(line.Length/2).ToList();
    counter += sack1.Intersect(sack2).Sum(GetValue);
}
Console.WriteLine($"Part1: {counter}");

counter = 0;
for (var i = 0; i < lines.Length/3; i++)
{
    var sack1 = lines[i*3].ToCharArray();
    var sack2 = lines[i*3+1].ToCharArray();
    var sack3 = lines[i*3+2].ToCharArray();
    counter += sack1.Intersect(sack2).Intersect(sack3).Sum(GetValue);
}
Console.WriteLine($"Part2: {counter}");

static int GetValue(char value)
{
    return char.IsLower(value) ? value - 'a' + 1 : value - 'A' + 27;
}