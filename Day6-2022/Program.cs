string InputFilePath = "/Users/xxxx/Documents/test.txt";
var input = File.ReadAllText(InputFilePath).ToString();

char[] characters = input.ToCharArray();

//part one
for (int v = 4; v < characters.Length; v++)
{
    var start = v; var end = v - 4;
    var previousFour = characters[end..start];

    if (previousFour.Distinct().Count() == 4)
    {
        Console.WriteLine(v);
        break;
    }

}

//part two

for (int v = 14; v < characters.Length; v++)
{
    var start = v; var end = v - 14;
    var previousFourteen = characters[end..start];

    if (previousFourteen.Distinct().Count() == 14) Console.WriteLine(v);
}