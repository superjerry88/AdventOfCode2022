var lines = File.ReadAllLines("input.txt");

var counter = 0;
var counter2 = 0;
foreach (var line in lines)
{
    var pair = line.Split(',');
    var a1 = int.Parse(pair[0].Split("-")[0]) ;
    var a2 = int.Parse(pair[0].Split("-")[1]);
    var b1 = int.Parse(pair[1].Split("-")[0]);
    var b2 = int.Parse(pair[1].Split("-")[1]);

    if ((a1 <= b1 && a2  >= b2) || (b1 <= a1 && b2 >= a2))
    {
        counter++;
    }

    if ((a2 >= b1 && b1>=a1) || (b2 >= a1 && a1 >= b1))
    {
        counter2++;
    }
}
Console.WriteLine($"Part1: {counter}");
Console.WriteLine($"Part1: {counter2}");