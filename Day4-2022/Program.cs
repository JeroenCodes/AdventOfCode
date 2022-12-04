string InputFilePath = "/Users/xxxx/Documents/test.txt";
var input = File.ReadAllText(InputFilePath).Split("\r\n");

int fullyoverlap = 0;
int overlap = 0;

//part one
foreach (var pair in input)
{
    var array = pair.Split(',');

    if (array.Length == 2)
    {


        var array_1 = array[0].Split('-');
        var array_2 = array[1].Split('-');

        var array_1_start = int.Parse(array_1[0]);
        var array_1_end = int.Parse(array_1[1]);
        var array_2_start = int.Parse(array_2[0]);
        var array_2_end = int.Parse(array_2[1]);


        if (array_1_start <= array_2_start && array_1_end >= array_2_end)
        {
            fullyoverlap++;
        }
        else if (array_2_start <= array_1_start && array_2_end >= array_1_end)
        {
            fullyoverlap++;
        }
    }

}
Console.WriteLine(fullyoverlap);

//part two
foreach (var pair in input) {
var array = pair.Split(',');

if (array.Length == 2)
{


    var array_1 = array[0].Split('-');
    var array_2 = array[1].Split('-');

    var array_1_start = int.Parse(array_1[0]);
    var array_1_end = int.Parse(array_1[1]);
    var array_2_start = int.Parse(array_2[0]);
    var array_2_end = int.Parse(array_2[1]);


    if (array_1_start <= array_2_start && array_1_end >= array_2_end)
    {
        overlap++;
    }
    else if (array_2_start <= array_1_start && array_2_end >= array_1_end)
    {
        overlap++;
    }
    else if(array_1_start <= array_2_start && array_2_start <= array_1_end)
        {
            overlap++;
        }
        else if (array_2_start <= array_1_start && array_1_start <= array_2_end)
        {
            overlap++;
        }
    }

}
Console.WriteLine(overlap);