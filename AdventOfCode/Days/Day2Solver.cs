namespace AdventOfCode.Days;

public class Day2Solver : ISolver
{
    private int _minDifference = 1;
    private int _maxDifference = 3;
    private string _part1Answer { get; set; }
    private string _part2Answer { get; set; }
    
    private List<List<int>> _reports = [];
    
    public void Setup(string[] input)
    {
        foreach (string line in input)
        {
            List<int> levels = line.Split(' ', StringSplitOptions.RemoveEmptyEntries)
                                    .Select(int.Parse)
                                    .ToList();
            _reports.Add(levels);
        }
    }

    public void SolvePart1()
    {
        int safeReports = 0;

        foreach (List<int> report in _reports)
        {
            if (IsValidReport(report))
            {
                safeReports++;
            }
        }
        
        _part1Answer = safeReports.ToString();
    }

    private bool IsValidReport(List<int> levels)
    {
        bool isValid = true;

        if (levels.Count < 2)
        {
            return true;
        }
            
        bool isIncreasing = levels.First() < levels.Last();

        for (int i = 0; i < levels.Count - 1; i++)
        {
            int currentLevel = levels[i];
            int nextLevel = levels[i + 1];
            int difference = isIncreasing
                ? nextLevel - currentLevel
                : currentLevel - nextLevel;

            if (difference < _minDifference || difference > _maxDifference)
            {
                return false;
            }
        }

        return isValid;
    }

    public void SolvePart2()
    {
        int safeReports = 0;

        foreach (List<int> report in _reports)
        {
            if (IsDamplyValidReport(report, null))
            {
                safeReports++;
            }
        }
        
        _part2Answer = safeReports.ToString();
    }
    
    private bool IsDamplyValidReport(List<int> levels, int? skipIndex)
    {
        bool isValid = true;
        
        int leftIndex = 0;
        if (leftIndex == skipIndex)
        {
            leftIndex++;
        }
        
        int rightIndex = leftIndex + 1;
        if (rightIndex == skipIndex)
        {
            rightIndex++;
        }
        
        // The increasing logic here is.. not good.
        bool isIncreasing = levels[leftIndex] < levels[rightIndex];
        if (levels.Count > 3)
        {
            int increasingSteps = 0;
            int decreasingSteps = 0;

            for (int i = 0; i < 4; i++)
            {
                if (levels[i] < levels[i + 1])
                {
                    increasingSteps++;
                }
                else
                {
                    decreasingSteps++;
                }
            }
            
            isIncreasing = increasingSteps > decreasingSteps;
        }

        while (leftIndex < rightIndex && rightIndex < levels.Count)
        {
            if (rightIndex == skipIndex)
            {
                rightIndex++;
                continue;
            }

            if (leftIndex == skipIndex)
            {
                leftIndex++;
                if (leftIndex == rightIndex)
                {
                    rightIndex++;
                }
                continue;
            }
            
            int leftLevel = levels[leftIndex];
            int rightLevel = levels[rightIndex];
            
            int difference = isIncreasing
                ? rightLevel - leftLevel
                : leftLevel - rightLevel;

            if (difference < _minDifference || difference > _maxDifference)
            {
                if (skipIndex.HasValue)
                {
                    return false;
                }

                bool isValidWithLeftSkip = IsDamplyValidReport(levels, leftIndex);
                bool isValidWithRightSkip = IsDamplyValidReport(levels, rightIndex);
                return isValidWithLeftSkip || isValidWithRightSkip;
            }
            
            leftIndex++;
            rightIndex++;
        }

        return isValid;
    }

    public void PrintAnswer()
    {
        Console.WriteLine(_part1Answer);
        Console.WriteLine(_part2Answer);
    }
}