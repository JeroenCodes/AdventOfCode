string InputFilePath = "/Users/xxxx/Documents/test.txt";
var input = File.ReadAllText(InputFilePath).ToString();

char[] characters = input.ToCharArray();

//part one
for (int v = 4; v < characters.Length; v++)
{
    var previousFour = new List<char>() { characters[v - 1], characters[v - 2], characters[v - 3], characters[v - 4] };

    if (previousFour.Distinct().Count() == 4)
    {
        Console.WriteLine(v);
        break;
    }

}

//part two

for (int v = 14; v < characters.Length; v++)
{
    var previousFourteen = new List<char>() { characters[v - 1], characters[v - 2], characters[v - 3], characters[v - 4], characters[v - 5], characters[v - 6], characters[v - 7], characters[v - 8], characters[v - 9], characters[v - 10], characters[v - 11], characters[v - 12], characters[v - 13], characters[v - 14] };

    if (previousFourteen.Distinct().Count() == 14) Console.WriteLine(v);
}