namespace AdventOfCode.Days;

public class Day4Solver : ISolver
{
    private string _part1Answer { get; set; }
    private string _part2Answer { get; set; }

    private int _rows;
    private int _cols;
    private string[] _input;
    
    private const string ForwardMatch = "XMAS";
    private const string BackwardMatch = "SAMX";
    private const int MatchLength = 4;
    public void Setup(string[] input)
    {
        _rows = input.Length;
        _cols = input.First().Length;
        
        _input = input;

        // for (int i = 0; i < _rows; i++)
        // {
        //     for (int j = 0; j < _cols; j++)
        //     {
        //         _input[i, j] = input[i][j];
        //     }
        // }
    }

    public void SolvePart1()
    {
        int count = 0;
        
        for (int i = 0; i < _rows; i++)
        {
            for (int j = 0; j < _cols; j++)
            {
                // Horizontal
                int horizontalForwardMatch =
                    IsPatternMatched(i, j, 0, 1, ForwardMatch, 0);
                int horizontalBackwardMatch =
                    IsPatternMatched(i, j, 0, 1, BackwardMatch, 0);
                
                // Vertical
                int verticalForwardMatch =
                    IsPatternMatched(i, j, 1, 0, ForwardMatch, 0);
                int verticalBackwardMatch =
                    IsPatternMatched(i, j, 1, 0, BackwardMatch, 0);
                
                // Diagonal 1
                int diagonal315ForwardMatch =
                    IsPatternMatched(i, j, 1, 1, ForwardMatch, 0);
                int diagonal315BackwardMatch =
                    IsPatternMatched(i, j, 1, 1, BackwardMatch, 0);

                // Diagonal 2
                int diagonal225ForwardMatch =
                    IsPatternMatched(i, j, 1, -1, ForwardMatch, 0);
                int diagonal225BackwardMatch =
                    IsPatternMatched(i, j, 1, -1, BackwardMatch, 0);

                int localSum =
                    horizontalForwardMatch +
                    horizontalBackwardMatch +
                    verticalForwardMatch +
                    verticalBackwardMatch +
                    diagonal315ForwardMatch +
                    diagonal315BackwardMatch +
                    diagonal225ForwardMatch +
                    diagonal225BackwardMatch;
                
                count += localSum;
            }
        }
        
        _part1Answer = count.ToString();
    }

    // Return 1 on match
    // return 0 if no match for easier summation.
    private int IsPatternMatched(
        int row, 
        int col, 
        int rowStep, 
        int colStep,
        string matchString,
        int matchIndex)
    {
        if (matchIndex == MatchLength)
        {
            return 1;
        }

        if ((row < 0 || row >= _rows) ||
            (col < 0 || col >= _cols) ||
            _input[row][col] != matchString[matchIndex])
        {
            return 0;
        }
        
        return IsPatternMatched(row + rowStep, col + colStep, rowStep, colStep, matchString, matchIndex + 1);
    }

    public void SolvePart2()
    {
        int count = 0;

        for (int i = 1; i < _rows - 1; i++)
        {
            for (int j = 1; j < _cols - 1; j++)
            {
                if (_input[i][j] != 'A')
                {
                    continue;
                }
                
                char topLeft = _input[i - 1][j - 1];
                if (topLeft is not ('M' or 'S'))
                {
                    continue;
                }
                
                char botRight = _input[i + 1][j + 1];
                if (botRight is not ('M' or 'S') ||
                    botRight == topLeft)
                {
                    continue;
                }
                
                char topRight = _input[i - 1][j + 1];
                if (topRight is not ('M' or 'S'))
                {
                    continue;
                }
                
                char botLeft = _input[i + 1][j - 1];
                if (botLeft is not ('M' or 'S') ||
                    botLeft == topRight)
                {
                    continue;
                }

                count++;
            }
        }
        
        _part2Answer = count.ToString();
    }

    public void PrintAnswer()
    {
        Console.WriteLine(_part1Answer);
        Console.WriteLine(_part2Answer);
    }
}