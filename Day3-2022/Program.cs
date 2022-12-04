string InputFilePath = "/Users/xxxx/Documents/test.txt";
var input = File.ReadAllText(InputFilePath).Split("\r\n");

char[] letters = new char[] { 'a','b','c','d','e','f','g','h','i','j','k','l','m','n','o','p','q','r','s','t','u','v','w','x','y','z' };

var score = 0;

//Part one
foreach (string backpack in input)
{
    var frontChars = backpack.Substring(0, (int)(backpack.Length / 2));
    var backChars = backpack.Substring((backpack.Length / 2), (int)(backpack.Length / 2));

    var common = frontChars.Intersect(backChars);

    if (common.Any())
    {
        foreach (char c in common)
        {
            bool matched = false;
            for (int v = 0; v < letters.Count(); v++)
            {
                if (letters[v] == c)
                {
                    score += v + 1;
                    matched = true;
                }
            }
            if (!matched)
            {
                for (int v = 0; v < letters.Count(); v++)
                {
                    var x = c.ToString().ToLower();
                    if (letters[v].ToString() == x) score += v + 27;
                    matched = true;
                }
            }
            Console.WriteLine(c);
            Console.WriteLine(score);
        }

    }
}

//part two
for (int i=0; i<input.Length;i+=3)
{
    //Take 3
    var group = input.Skip(i).Take(3).ToArray();
    Console.WriteLine(group[0]);
    Console.WriteLine(group[1]);
    Console.WriteLine(group[2]);
    Console.WriteLine(i);
    var common1 = group[0].Intersect(group[1]);
    var common = common1.Intersect(group[2]);

    if (common.Any())
    {
        foreach (char c in common)
        {
            bool matched = false;
            for (int v = 0; v < letters.Count(); v++)
            {
                if (letters[v] == c)
                {
                    score += v + 1;
                    matched = true;
                }
            }
            if (!matched)
            {
                for (int v = 0; v < letters.Count(); v++)
                {
                    var x = c.ToString().ToLower();
                    if (letters[v].ToString() == x) score += v + 27;
                    matched = true;
                }
            }
            
            Console.WriteLine(c);
            Console.WriteLine(score);
        }
        
    }

}