namespace AdventOfCode.Days;

public class Day3Solver : ISolver
{
    private bool _isEnabled = true;
    private string _part1Answer { get; set; }
    private string _part2Answer { get; set; }
    
    private string[] _input;
    public void Setup(string[] input)
    {
        _input = input;
    }

    public void SolvePart1()
    {
        int sum = 0;

        foreach (string line in _input)
        {
            sum += LineToSum(line);
        }
        
        _part1Answer = sum.ToString();
    }

    private static int LineToSum(string line)
    {
        if (string.IsNullOrWhiteSpace(line) || line.Length < 8)
        {
            return 0;
        }
        
        int sum = 0;
        int left = 0;
        int right = 1;
        string patternStart = "mul(";
        int patternStartLength = patternStart.Length;
        // mul(x,x)

        while (left < line.Length && right < line.Length)
        {
            // Whenever we find the opening character, try to find
            // the opening part of our pattern
            if (line[left] == 'm')
            {
                // Try to find the opening patten
                string slice = line.Substring(left, patternStartLength);
                if (slice != patternStart)
                {
                    left++;
                    continue;
                }
                
                // Try to parse out an int, up to the separator
                right = left + patternStartLength;
                int firstNumberValue = 0;
                bool commaSeparatorFound = false;
                
                while (commaSeparatorFound is false && right < line.Length)
                {
                    char character = line[right];
                    if (char.IsDigit(character))
                    {
                        firstNumberValue *= 10;
                        firstNumberValue += int.Parse(character.ToString());
                    }
                    else if (character == ',')
                    {
                        commaSeparatorFound = true;
                    }
                    else
                    {
                        break;
                    }
                    
                    right++;
                }

                if (commaSeparatorFound is false)
                {
                    left++;
                    continue;
                }
                
                // Try to parse out an int, up to the closing bracket
                int secondNumberValue = 0;
                bool bracketSeparatorFound = false;
                
                while (bracketSeparatorFound is false && right < line.Length)
                {
                    char character = line[right];
                    if (char.IsDigit(character))
                    {
                        secondNumberValue *= 10;
                        secondNumberValue += int.Parse(character.ToString());
                    }
                    else if (character == ')')
                    {
                        bracketSeparatorFound = true;
                    }
                    else
                    {
                        break;
                    }
                    
                    right++;
                }

                if (bracketSeparatorFound is false)
                {
                    left++;
                    continue;
                }
                
                int mulResult = firstNumberValue * secondNumberValue;
                sum += mulResult;
                left = right;
                continue;
            }

            left++;
        }
        
        return sum;
    }

    public void SolvePart2()
    {
        int sum = 0;

        foreach (string line in _input)
        {
            sum += LineToSumWithConditions(line);
        }
        
        _part2Answer = sum.ToString();
    }
    
    private int LineToSumWithConditions(string line)
    {
        if (string.IsNullOrWhiteSpace(line) || line.Length < 8)
        {
            return 0;
        }
        
        int sum = 0;
        int left = 0;
        int right = 1;
        const string patternStart = "mul(";
        int patternStartLength = patternStart.Length;
        // mul(x,x)
        
        const string doPatten = "do()";
        const string dontPattern = "don't()";

        while (left < line.Length && right < line.Length)
        {
            // Whenever we find the opening character, try to find
            // the opening part of our pattern
            if (line[left] == 'm')
            {
                if (_isEnabled is false)
                {
                    left++;
                    continue;
                }
                
                // Try to find the opening patten
                string slice = line.Substring(left, patternStartLength);
                if (slice != patternStart)
                {
                    left++;
                    continue;
                }
                
                // Try to parse out an int, up to the separator
                right = left + patternStartLength;
                int firstNumberValue = 0;
                bool commaSeparatorFound = false;
                
                while (commaSeparatorFound is false && right < line.Length)
                {
                    char character = line[right];
                    if (char.IsDigit(character))
                    {
                        firstNumberValue *= 10;
                        firstNumberValue += int.Parse(character.ToString());
                    }
                    else if (character == ',')
                    {
                        commaSeparatorFound = true;
                    }
                    else
                    {
                        break;
                    }
                    
                    right++;
                }

                if (commaSeparatorFound is false)
                {
                    left++;
                    continue;
                }
                
                // Try to parse out an int, up to the closing bracket
                int secondNumberValue = 0;
                bool bracketSeparatorFound = false;
                
                while (bracketSeparatorFound is false && right < line.Length)
                {
                    char character = line[right];
                    if (char.IsDigit(character))
                    {
                        secondNumberValue *= 10;
                        secondNumberValue += int.Parse(character.ToString());
                    }
                    else if (character == ')')
                    {
                        bracketSeparatorFound = true;
                    }
                    else
                    {
                        break;
                    }
                    
                    right++;
                }

                if (bracketSeparatorFound is false)
                {
                    left++;
                    continue;
                }
                
                int mulResult = firstNumberValue * secondNumberValue;
                sum += mulResult;
                left = right;
                continue;
            }
            
            if (line[left] == 'd')
            {
                string doSlice = line.Substring(left, doPatten.Length);
                string dontSlice = line.Substring(left, dontPattern.Length);

                if (dontSlice == dontPattern)
                {
                    _isEnabled = false;
                    left += dontPattern.Length;
                    continue;
                }
                
                if (doSlice == doPatten)
                {
                    _isEnabled = true;
                    left += doPatten.Length;
                    continue;
                }
            }

            left++;
        }
        
        return sum;
    }

    public void PrintAnswer()
    {
        Console.WriteLine(_part1Answer);
        Console.WriteLine(_part2Answer);
    }
}