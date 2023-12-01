Dictionary<string, int> _spellingToDigitMapping = new()
{
    ["one"] = 1,
    ["two"] = 2,
    ["three"] = 3,
    ["four"] = 4,
    ["five"] = 5,
    ["six"] = 6,
    ["seven"] = 7,
    ["eight"] = 8,
    ["nine"] = 9,
};

Console.Write("Input File: ");
var inputFilePath  = Console.ReadLine();

var lines = new List<string>();
using(var reader = new StreamReader(inputFilePath))
{
    string? line;
    do
    {
        line = await reader.ReadLineAsync().ConfigureAwait(false);
        if (line != null)
        {
            lines.Add(line);
        }
    }while(line != null);
}

//var calibrationValues = lines.Select(CalculateCalibrationValue).ToList();
//var part1Result = calibrationValues.Sum();
//Console.WriteLine(part1Result);

var calibrationValues2 = lines.Select(CalculateCalibrationValue2).ToList();
var part2Result = calibrationValues2.Sum();
Console.WriteLine(part2Result);

int CalculateCalibrationValue(string line)
{
    int? first = null, last = null;
    foreach(char character in line)
    {
        if(character >= 48 && character <= 57)
        {
            if (first.HasValue)
            {
                last = character - 48;
            }
            else
            {
                first = character - 48;
            }
        }
    }

    if (!first.HasValue)
    {
        throw new ArgumentException("Line should contain at least one digit", nameof(line));
    }

    if (!last.HasValue)
    {
        last = first;
    }

    return 10 * first.Value + last.Value;
}

int CalculateCalibrationValue2(string line)
{
    int? first = null, last = null;
    for(int i = 0; i<line.Length;i++)
    {
        char character = line[i];
        int? digit = null;
        if (character >= 48 && character <= 57)
        {
            digit = character - 48;
        }
        else
        {
            var sub = line.Substring(i, line.Length - i);
            foreach (var mapping in _spellingToDigitMapping)
            {
                if (sub.StartsWith(mapping.Key))
                {
                    digit = mapping.Value;
                    break;
                }
            }
        }

        if (digit.HasValue)
        {
            if (first.HasValue)
            {
                last = digit;
            }
            else
            {
                first = digit;
            }
        }
    }

    if (!first.HasValue)
    {
        throw new ArgumentException("Line should contain at least one digit", nameof(line));
    }

    if (!last.HasValue)
    {
        last = first;
    }

    return 10 * first.Value + last.Value;
}
