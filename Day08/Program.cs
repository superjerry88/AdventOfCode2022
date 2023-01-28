var lines = File.ReadAllLines("input.txt");

var rowCount = lines.Length;
var columnCount = lines[0].Length;

var map = new int[rowCount,columnCount];
for (var row = 0; row < lines.Length; row++)
{
    for (var col = 0; col < lines[row].Length; col++)
    {
        map[row,col]  = int.Parse(lines[row][col].ToString());
    }
}

var treeCounter = 0;
for (var row = 0; row < rowCount; row++)
{
    for (var col = 0; col < columnCount; col++)
    {
        if (IsVisible(row, col)) treeCounter++;
    }
}
Console.WriteLine($"Part1: {treeCounter}");

var best = 0;
for (var row = 0; row < rowCount; row++)
{
    for (var col = 0; col < columnCount; col++)
    {
        var score= GetScore(row, col);
        if(score > best) best = score;
    }
}
Console.WriteLine($"Part2: {best}");

bool IsVisible(int x, int y)
{
    if (x == 0 || x == rowCount-1 || y == 0 || y == rowCount-1) return true;
    var value = map[x, y];
    var row = GetRow(x);
    var col = GetColumn(y);

    if (row.Take(y).All(c => c < value)) return true;
    if (row.Skip(y + 1).All(c => c < value)) return true;
    if (col.Take(x).All(c => c < value)) return true;
    if (col.Skip(x + 1).All(c => c < value)) return true;

    return false;
}

int GetScore(int x, int y)
{
    var value = map[x, y];
    var row = GetRow(x);
    var col = GetColumn(y);

    var left = CountVisibleTree(row.Take(y).Reverse(), value);
    var right = CountVisibleTree(row.Skip(y + 1), value);
    var up = CountVisibleTree(col.Take(x).Reverse(), value);
    var down = CountVisibleTree(col.Skip(x + 1), value);

    return left * right * up * down;
}

int CountVisibleTree(IEnumerable<int> trees, int currentHeight)
{
    return trees.TakeWhile(tree => tree < currentHeight).Count() + (trees.Any(tree => tree >= currentHeight) ? 1 : 0);
}

List<int> GetColumn(int row)
{
    return Enumerable.Range(0, map.GetLength(0))
        .Select(x => map[x, row])
        .ToList();
}

List<int> GetRow(int row)
{
    return Enumerable.Range(0, map.GetLength(1))
        .Select(x => map[row, x])
        .ToList();
}

