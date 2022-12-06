using System.Text.RegularExpressions;

internal class Program
{
    private static void Main(string[] args)
    {
        string InputFilePath = "/Users/xxxx/Documents/test.txt";
        var input = File.ReadAllText(InputFilePath).Split("\r\n");

        var list_1 = new List<char> { 'S', 'M', 'R', 'N', 'W', 'J', 'V', 'T' };
        var list_2 = new List<char> { 'B', 'W', 'D', 'J', 'Q', 'P', 'C', 'V' };
        var list_3 = new List<char> { 'B', 'J', 'F', 'H', 'D', 'R', 'P' };
        var list_4 = new List<char> { 'F', 'R', 'P', 'B', 'M', 'N', 'D' };
        var list_5 = new List<char> { 'H', 'V', 'R', 'P', 'T', 'B' };
        var list_6 = new List<char> { 'C', 'B', 'P', 'T' };
        var list_7 = new List<char> { 'B', 'J', 'R', 'P', 'L' };
        var list_8 = new List<char> { 'N', 'C', 'S', 'L','T', 'Z', 'B', 'W' };
        var list_9 = new List<char> { 'L', 'S', 'G' };

        foreach (string move in input)
        {
            if (move == "") break;
            var numbers = move.TrimStart('m', 'o', 'v', 'e', ' ');
            var moveNumber = int.Parse(Regex.Match(numbers, @"^\d+").ToString());

            var from = char.GetNumericValue(numbers.TakeLast(6).First());
            var to = char.GetNumericValue(numbers.TakeLast(1).FirstOrDefault());

            List<char> movingFrom;
            switch (from)
            {
                case 1: movingFrom = list_1; break;
                case 2: movingFrom = list_2; break;
                case 3: movingFrom = list_3; break;
                case 4: movingFrom = list_4; break;
                case 5: movingFrom = list_5; break;
                case 6: movingFrom = list_6; break;
                case 7: movingFrom = list_7; break;
                case 8: movingFrom = list_8; break;
                case 9: movingFrom = list_9; break;
                default: movingFrom = list_1; break;
            }

            List<char> movingBoxes;
            if (moveNumber >= movingFrom.Count)
            {
                movingBoxes = movingFrom.Take(movingFrom.Count).Reverse().ToList();
            }
            else
            {
                movingBoxes = movingFrom.TakeLast(moveNumber).Reverse().ToList();
            }

            switch (from)
            {
                case 1: list_1.RemoveRange(list_1.Count - movingBoxes.Count, movingBoxes.Count); Console.WriteLine(string.Join(", ", list_1)); ; break;
                case 2: list_2.RemoveRange(list_2.Count - movingBoxes.Count, movingBoxes.Count); Console.WriteLine(string.Join(", ", list_2)); ; break;
                case 3: list_3.RemoveRange(list_3.Count - movingBoxes.Count, movingBoxes.Count); Console.WriteLine(string.Join(", ", list_3)); ; break;
                case 4: list_4.RemoveRange(list_4.Count - movingBoxes.Count, movingBoxes.Count); Console.WriteLine(string.Join(", ", list_4)); ; break;
                case 5: list_5.RemoveRange(list_5.Count - movingBoxes.Count, movingBoxes.Count); Console.WriteLine(string.Join(", ", list_5)); ; break;
                case 6: list_6.RemoveRange(list_6.Count - movingBoxes.Count, movingBoxes.Count); Console.WriteLine(string.Join(", ", list_6)); ; break;
                case 7: list_7.RemoveRange(list_7.Count - movingBoxes.Count, movingBoxes.Count); Console.WriteLine(string.Join(", ", list_7)); ; break;
                case 8: list_8.RemoveRange(list_8.Count - movingBoxes.Count, movingBoxes.Count); Console.WriteLine(string.Join(", ", list_8)); ; break;
                case 9: list_9.RemoveRange(list_9.Count - movingBoxes.Count, movingBoxes.Count); Console.WriteLine(string.Join(", ", list_9)); ; break;
                default: foreach (char c in movingBoxes) { list_1.Remove(c); }; break;
            }

            switch (to)
            {
                case 1: list_1.AddRange(movingBoxes); Console.WriteLine(string.Join(", ", list_1)); break;
                case 2: list_2.AddRange(movingBoxes); Console.WriteLine(string.Join(", ", list_2)); break;
                case 3: list_3.AddRange(movingBoxes); Console.WriteLine(string.Join(", ", list_3)); break;
                case 4: list_4.AddRange(movingBoxes); Console.WriteLine(string.Join(", ", list_4)); break;
                case 5: list_5.AddRange(movingBoxes); Console.WriteLine(string.Join(", ", list_5)); break;
                case 6: list_6.AddRange(movingBoxes); Console.WriteLine(string.Join(", ", list_6)); break;
                case 7: list_7.AddRange(movingBoxes); Console.WriteLine(string.Join(", ", list_7)); break;
                case 8: list_8.AddRange(movingBoxes); Console.WriteLine(string.Join(", ", list_8)); break;
                case 9: list_9.AddRange(movingBoxes); Console.WriteLine(string.Join(", ", list_9)); break;
                default: list_1.AddRange(movingBoxes); break;
            }

        }

        Console.WriteLine("Results:");
        Console.WriteLine("Box 1: " + string.Join(", ", list_1));
        Console.WriteLine("Box 2: " + string.Join(", ", list_2));
        Console.WriteLine("Box 3: " + string.Join(", ", list_3));
        Console.WriteLine("Box 4: " + string.Join(", ", list_4));
        Console.WriteLine("Box 5: " + string.Join(", ", list_5));
        Console.WriteLine("Box 6: " + string.Join(", ", list_6));
        Console.WriteLine("Box 7: " + string.Join(", ", list_7));
        Console.WriteLine("Box 8: " + string.Join(", ", list_8));
        Console.WriteLine("Box 9: " + string.Join(", ", list_9));
    }
}

//part two - just remove the .Reverse on line 65 and 69