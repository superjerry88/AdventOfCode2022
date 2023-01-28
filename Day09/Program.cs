var lines = File.ReadAllLines("input.txt");

var head = new Pos();
var tail = new Pos();
var rope = Enumerable.Range(0, 9).Select(x => new Pos()).ToList();

var counter1 = new HashSet<string>();
var counter2 = new HashSet<string>();
foreach (var line in lines)
{
    var value = int.Parse(line.Split(" ")[1]);
    for (var i = 0; i < value; i++)
    {
        switch (line[0])
        {
            case 'U':
                head.Y ++;
                break;
            case 'D':
                head.Y--;
                break;
            case 'L':
                head.X --;
                break;
            case 'R':
                head.X ++;
                break;
        }
        
        MoveTail(head,tail);
        MoveTail(head, rope[0]);
        for (var length = 1; length < rope.Count; length++)
        {
            MoveTail(rope[length - 1], rope[length]);
        }
        counter2.Add(rope.Last().ToString());
        counter1.Add(tail.ToString());
    }
}

Console.WriteLine($"Part1: {counter1.Count}");
Console.WriteLine($"Part2: {counter2.Count}");

void MoveTail( Pos start, Pos end)
{
    if (Math.Abs(end.X - start.X) == 1 && Math.Abs(end.Y - start.Y) == 1)
    {
        //this
    }
    else if (Math.Abs(end.X - start.X) == 0 && Math.Abs(end.Y - start.Y) == 1)
    {
        //can probably
    }
    else if (Math.Abs(end.X - start.X) == 1 && Math.Abs(end.Y - start.Y) == 0)
    {
        //be simplified
    }
    else if (end.X + 2 == start.X && end.Y == start.Y)
    {
        end.X++;
    }
    else if (end.X - 2 == start.X && end.Y == start.Y)
    {
        end.X--;
    }
    else if (end.Y + 2 == start.Y && end.X == start.X)
    {
        end.Y++;
    }
    else if (end.Y - 2 == start.Y && end.X == start.X)
    {
        end.Y--;
    }
    else if (start.X > end.X && start.Y > end.Y)
    {
        end.X++;
        end.Y++;
    }
    else if (start.X > end.X && start.Y < end.Y)
    {
        end.X++;
        end.Y--;
    }
    else if (start.X < end.X && start.Y > end.Y)
    {
        end.X--;
        end.Y++;
    }
    else if (start.X < end.X && start.Y < end.Y)
    {
        end.X--;
        end.Y--;
    }
}

class Pos
{
    public int X, Y;
    public override string ToString()
    {
        return $"[{X}][{Y}]";
    }
}
