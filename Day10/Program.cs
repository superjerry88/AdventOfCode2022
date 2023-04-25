var lines = File.ReadAllLines("input.txt");

var importantCycle = new List<int>{20,60,100,140,180,220};

var tick = 0;
var x =1;
var counter1 = 0;

foreach (var line in lines)
{
    if (line == "noop")
    {
        Draw();
    }
    else
    {
        Draw();
        Draw();
        if (importantCycle.Count == 0) break;
        var cycle = importantCycle.First();
        if (tick >= cycle)
        {
            importantCycle.RemoveAt(0);
            counter1 += x * cycle;
            Console.WriteLine($"Cycle: {cycle} - {x} - {x*cycle}");
        }
        x += int.Parse(line.Replace("addx ", ""));
    }
}



var width = 40;
var height = 6;

List<string> display = new List<string>();
void Draw()
{
    tick++;
    var cursor = tick % 40;
    Console.WriteLine($"cursor: {cursor}");


}

Console.WriteLine($"Part1: {counter1}");