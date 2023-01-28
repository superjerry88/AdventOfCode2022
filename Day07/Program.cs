var lines = File.ReadAllLines("input.txt").ToList();

var root = new Dir("/");
var current = root;

foreach (var line in lines)
{
    if (line == "$ cd ..")
    {
        current = current.Parent;
    }
    else if (line == "$ cd /")
    {
        current = root;
    }
    else if (line.StartsWith("$ cd"))
    {
        current = current.Subs.First(c => c.Name == line.Replace("$ cd ", ""));
    }
    else if (line == "$ ls")
    {
        //hi
    }
    else if (line.StartsWith("dir"))
    {
        current.Subs.Add(new Dir(line.Replace("dir ", ""),current));
    }
    else
    {
        current.Size += int.Parse(line.Split(" ")[0]);
    }
}

var counter = root.Flatten.Where(c => c.TotalSize <= 100000).Sum(dir => dir.TotalSize);
Console.WriteLine($"Part1: {counter}");

var allDir = root.Flatten.Concat(new List<Dir> {root}).ToList();
var usedSpace = allDir.Sum(c => c.Size);
var neededToDelete =  usedSpace - 40000000;
var selected = allDir.OrderBy(c => c.TotalSize).First(c => c.TotalSize > neededToDelete);
Console.WriteLine($"Part2: {selected.TotalSize}");

internal class Dir
{
    public Dir(string name, Dir parent = null)
    {
        Name = name;
        Parent = parent;
    }

    public List<Dir> Subs { get; set; } = new List<Dir>();
    public Dir Parent { get; set; }
    public string Name { get; set; }
    public int Size { get; set; }
    public int TotalSize => Size + Subs.Select(c=>c.TotalSize).Sum();
    public List<Dir> Flatten =>  Subs.SelectMany(c => c.Flatten).Concat(Subs).ToList();
}