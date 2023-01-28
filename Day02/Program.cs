/*
 * A | X - Rock (1)
 * B | Y - Paper (2)
 * C | Z - Scissors (3)
 */
var lines = File.ReadAllLines("input.txt");

var counter = 0;
foreach (var line in lines)
{
    switch (line)
    {
        case "A X":
            counter += 3+1;
            break;
        case "A Y":
            counter += 6+2;
            break;
        case "A Z":
            counter += 0+3;
            break;
        case "B X":
            counter += 0+1;
            break;
        case "B Y":
            counter += 3+2;
            break;
        case "B Z":
            counter += 6+3;
            break;
        case "C X":
            counter += 6+1;
            break;
        case "C Y":
            counter += 0+2;
            break;
        case "C Z":
            counter += 3+3;
            break;
    }
}
Console.WriteLine($"Part1: {counter}");

counter = 0;
foreach (var line in lines)
{
    switch (line)
    {
        //I need to lose
        case "A X":
            counter += 0 + 3;
            break;
        case "B X":
            counter += 0 + 1;
            break;
        case "C X":
            counter += 0 + 2;
            break;
        //I need to draw
        case "A Y":
            counter += 3 + 1;
            break;
        case "B Y":
            counter += 3 + 2;
            break;
        case "C Y":
            counter += 3 + 3;
            break;
        //I need to win
        case "A Z":
            counter += 6 + 2;
            break;
        case "B Z":
            counter += 6 + 3;
            break;
        case "C Z":
            counter += 6 + 1;
            break;
    }
}
Console.WriteLine($"Part2: {counter}");