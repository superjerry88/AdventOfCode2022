var lines = File.ReadAllLines("input.txt").ToList();

var splitter = lines.FindIndex( string.IsNullOrEmpty);
var range = lines[splitter - 2].Count(c => c=='[');
var cargo = new Dictionary<int, Stack<char>>();
var cargo2 = new Dictionary<int, List<char>>();
for (var i = 0; i < range; i++)
{
    cargo[i] = new Stack<char>();
    cargo2[i] = new List<char>();
}

foreach (var line in lines.Take(range).Reverse())
{
    var cursor = 1;
    for (var i = 0; i < range; i++)
    {
        var item = line[cursor];
        if (!char.IsSeparator(item))
        {
            cargo[i].Push(item);
            cargo2[i].Add(item);
        }
        cursor += 4;
    }
}

foreach (var line in lines.Skip(splitter+1))
{
    var word = line.Split(" ");
    var amount = int.Parse(word[1]);
    var from = int.Parse(word[3])-1;
    var to = int.Parse(word[5])-1;

    //part1
    for (var i = 0; i < amount; i++)
    {
        cargo[to].Push(cargo[from].Pop());
    }

    //part2
    cargo2[to].AddRange(cargo2[from].TakeLast(amount));
    cargo2[from].RemoveRange(cargo2[from].Count-amount,amount);
}

Console.Write("Part1:");
foreach (var c in cargo) Console.Write(c.Value.Pop());

Console.Write("\nPart2:");
foreach (var c in cargo2) Console.Write(c.Value.Last());
