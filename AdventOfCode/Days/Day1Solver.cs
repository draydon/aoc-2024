namespace AdventOfCode.Days;

public class Day1Solver : ISolver
{
    private string _part1Answer { get; set; }
    private string _part2Answer { get; set; }
    
    private List<int> _firstList = [];
    private List<int> _secondList = [];
    
    public void Setup(string[] input)
    {
        foreach (string line in input)
        {
            string[] numbers = line.Split(" ", StringSplitOptions.RemoveEmptyEntries);
            int first = int.Parse(numbers[0]);
            int second = int.Parse(numbers[1]);
            
            _firstList.Add(first);
            _secondList.Add(second);
        }
    }

    public void SolvePart1()
    {
        int distance = 0;
        
        _firstList.Sort();
        _secondList.Sort();

        for (int i = 0; i < _firstList.Count; i++)
        {
            int absoluteDistance = Math.Abs(_firstList[i] - _secondList[i]);
            distance += absoluteDistance;
        }
        
        _part1Answer = distance.ToString();
    }

    public void SolvePart2()
    {
        Dictionary<string, int> frequencies = [];
        int similarityScore = 0;

        foreach (int number in _firstList)
        {
            frequencies.Add(number.ToString(), 0);
        }

        foreach (int number in _secondList)
        {
            if (frequencies.ContainsKey(number.ToString()) is false)
            {
                continue;
            }
            
            frequencies[number.ToString()]++;
        }

        foreach (int number in _firstList)
        {
            similarityScore += (number * frequencies[number.ToString()]);
        }
        
        _part2Answer = similarityScore.ToString();
    }

    public void PrintAnswer()
    {
        Console.WriteLine(_part1Answer);
        Console.WriteLine(_part2Answer);
    }
}