string InputFilePath = "/Users/xxxx/Documents/test.txt";
var gamesList = File.ReadAllText(InputFilePath).Split("\r\n");

Console.WriteLine(gamesList.FirstOrDefault());
int score = 0;
int highestscore = 0;

//a rock, b paper, c scissor, x rock, y paper, z scissor
//a z = loss,
string[] losses = new string[] { "A Z", "B X", "C Y" };
string[] wins = new string[] { "A Y", "B Z", "C X" };
string[] draws = new string[] { "A X", "B Y", "C Z" };
int gamesScore = 0;
List<int> gamescores = new List<int>();
//1,2,3, points
foreach (var game in gamesList)
{
    gamesScore = 0;
    if (game.EndsWith("X"))
    {
        score = score + 1;
        gamesScore++;
    }
    if (game.EndsWith("Y"))
    {
        score = score + 2;
        gamesScore = gamesScore + 2;
    }
    if (game.EndsWith("Z"))
    {
        score = score + 3;
        gamesScore = gamesScore + 3;
    }

    if (losses.Any(s => s.Contains(game)))
    {
        //loss o score
        score = score + 0;
        gamesScore = gamesScore + 0;
    }
    else
    if (wins.Any(s => s.Contains(game)))
    {
        //win
        score = score + 6;
        gamesScore = gamesScore + 6;
    }
    else
    if (draws.Any(s => s.Contains(game)))
    {
        //draw
        score = score + 3;
        gamesScore = gamesScore + 3;
    }
    Console.WriteLine(gamesScore);
    gamescores.Add(gamesScore);

}
Console.WriteLine(gamescores);
Console.WriteLine(gamescores.Sum());
Console.WriteLine(score);

//round two
string[] needToLose = new string[] { "A Z", "B X", "C Y" };
string[] NeedToWin = new string[] { "A Y", "B Z", "C X" };
string[] NeedToDraw = new string[] { "A X", "B Y", "C Z" };
int gamesScore2 = 0;
List<int> gamescores2 = new List<int>();
//1,2,3, points
foreach (var game in gamesList)
{
    gamesScore2 = 0;
    //need to lsoe
    if (game.EndsWith("X"))
    {
        if (game.StartsWith("A"))
        {
            gamesScore2 = gamesScore2 + 3;
        }
        if (game.StartsWith("B"))
        {
            gamesScore2 = gamesScore2 + 1;
        }
        if (game.StartsWith("C"))
        {
            gamesScore2 = gamesScore2 + 2;
        }
        //need to lose
    }
    if (game.EndsWith("Y"))
    {
        //need to draw
        if (game.StartsWith("A"))
        {
            gamesScore2 = gamesScore2 + 1;
        }
        if (game.StartsWith("B"))
        {
            gamesScore2 = gamesScore2 + 2;
        }
        if (game.StartsWith("C"))
        {
            gamesScore2 = gamesScore2 + 3;
        }
        gamesScore2 = gamesScore2 + 3;
    }
    if (game.EndsWith("Z"))
    {
        //need to win
        if (game.StartsWith("A"))
        {
            gamesScore2 = gamesScore2 + 2;
        }
        if (game.StartsWith("B"))
        {
            gamesScore2 = gamesScore2 + 3;
        }
        if (game.StartsWith("C"))
        {
            gamesScore2 = gamesScore2 + 1;
        }
        gamesScore2 = gamesScore2 + 6;
    }
    gamescores2.Add(gamesScore2);

}

Console.WriteLine(gamescores2.Sum());
Console.WriteLine(score);
