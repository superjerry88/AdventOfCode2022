using System.Diagnostics;

var lines = File.ReadAllLines("input.txt");

var p1StartPos = int.Parse(lines[0].Split(":")[1].Trim());
var p2StartPos = int.Parse(lines[1].Split(":")[1].Trim());


//Part1 : List all players 
List<Part1Player> players = new()
{
    new Part1Player("1", p1StartPos),
    new Part1Player("2", p2StartPos),
};

//Part1 : Each player take a move until there's a winner
while (players.All(c=>!c.HasWon))
{
    foreach (var player in players)
    {
        player.Move();
    }
}

//Part1 : Print out each player's score and win status 
foreach (var player in players)
{
    Console.WriteLine($"Part1: Player{player.Name} [Won: {player.HasWon}], Combine Score: {player.CombineScore}");
}

//Part2 
var stopwatch = Stopwatch.StartNew();
Part2Board.Start(p1StartPos, p2StartPos);
Console.WriteLine($"Part2: Player1 [Won: {Part2Board.P1WinCounter}], Player2 [Won: {Part2Board.P2WinCounter}]. Time Taken: {stopwatch.ElapsedMilliseconds}ms");


public class Part1Player
{
    //Configurations 
    public static int MinBoardSize = 1;
    public static int MaxBoardSize = 10;
    public static int DiceSide = 100;
    public static int DicePerRound = 3;
    public static int WinPoint = 1000;

    //Counters
    public static int DicePointer = 1;
    public static int DiceThrew = 0;

    public string Name;
    public int Pos;
    public int TotalPoint;

    public bool HasWon => TotalPoint >= WinPoint;
    public int CombineScore => TotalPoint * DiceThrew;

    public Part1Player(string name, int startPos)
    {
        Name = name;
        Pos = startPos;
    }

    public void Move()
    {
        var score = 0;
        for (var i = 0; i < DicePerRound; i++)
        {
            score += DicePointer++;
            if (DicePointer > DiceSide) DicePointer = 1;
        }

        Pos = (Pos + score) % MaxBoardSize;
        if (Pos > MaxBoardSize) Pos = MinBoardSize;
        if (Pos < MinBoardSize) Pos = MaxBoardSize;//0 is 10 
        
        DiceThrew += DicePerRound;
        TotalPoint += Pos;
    }
}

public class Part2Board
{
    //Configurations
    public static int MinBoardSize = 1;
    public static int MaxBoardSize = 10;
    public static int WinningPoint = 21;
    public static int DiceCount = 3;
    public static int DiceSide = 3;

    //Counters
    public static long P1WinCounter = 0;
    public static long P2WinCounter = 0;

    public static Dictionary<int, long> UniqueUniverse = new Dictionary<int, long>();

    struct Player
    {
        public int Score { get; set; }
        public int Pos { get; set; }

        public bool Won => Score >= WinningPoint;
    }

    public static void Start(int p1StartPosition, int p2StartPosition)
    {
        var p1 = new Player() { Pos = p1StartPosition };
        var p2 = new Player() { Pos = p2StartPosition };

        //Creating the universe multiplier
        //Can be simplified if hardcoded
        for (var i = 1; i <= Math.Pow(DiceSide, DiceCount); i++)
        {
            var diceValues = new int[DiceCount];
            var remaining = i - 1;
            for (var j = 0; j < DiceCount; j++)
            {
                diceValues[j] = remaining % DiceSide + 1;
                remaining /= DiceSide;
            }
            var sum = diceValues.Sum();
            if (!UniqueUniverse.ContainsKey(sum)) UniqueUniverse[sum] = 0;
            UniqueUniverse[sum] += 1;
        }

        //Start throwing dice starting from player 1
        Player1Move(p1, p2, 1);
    }

    static void Player1Move(Player p1, Player p2, long multiplier)
    {
        //Create 7 unique universe on each move
        foreach (var universe in UniqueUniverse)
        {
            var diceRoll = universe.Key;
            var totalUniverses = universe.Value * multiplier;

            var p = p1;
            p.Pos = CountPoints(p1.Pos, diceRoll);
            p.Score += p.Pos;
           
            if (p.Won)
            {
                P1WinCounter += totalUniverses;
            }
            else
            {
                Player2Move(p, p2, totalUniverses);
            }
        }
    }

    static void Player2Move(Player p1, Player p2, long multiplier)
    {
        //Create 7 unique universe on each move
        foreach (var universe in UniqueUniverse)
        {
            var diceRoll = universe.Key;
            var totalUniverses = universe.Value * multiplier;

            var p = p2;
            p.Pos = CountPoints(p2.Pos, diceRoll);
            p.Score += p.Pos;

            if (p.Won)
            {
                P2WinCounter += totalUniverses;
            }
            else
            {
                Player1Move(p1, p, totalUniverses);
            }
        }
    }

    public static int CountPoints(int currentPos, int posToMove)
    {
        currentPos = (currentPos + posToMove) % MaxBoardSize;
        if (currentPos > MaxBoardSize) currentPos = MinBoardSize;
        if (currentPos < MinBoardSize) currentPos = MaxBoardSize;//0 is 10 
        return currentPos;
    }
}
