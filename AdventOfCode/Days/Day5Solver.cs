using AdventOfCode.Utilities;

namespace AdventOfCode.Days;

public class Day5Solver : ISolver
{
    private string _part1Answer { get; set; }
    private string _part2Answer { get; set; }
    
    Dictionary<int, HashSet<int>> _rules = [];
    List<List<int>> _updates = [];
    
    public void Setup(string[] input)
    {
        bool isLoadingRules = true;
        
        foreach (string line in input)
        {
            if (string.IsNullOrWhiteSpace(line))
            {
                isLoadingRules = false;
                continue;
            }

            if (isLoadingRules)
            {
                string[] parts = line.Split('|');
                int key = int.Parse(parts[0]);
                int value = int.Parse(parts[1]);

                if (_rules.ContainsKey(key))
                {
                    _rules[key].Add(value);
                }
                else
                {
                    _rules.Add(key, new HashSet<int> { value });
                }
            }
            else
            {
                string[] parts = line.Split(',', StringSplitOptions.RemoveEmptyEntries);
                List<int> update = parts.Select(x => int.Parse(x)).ToList();
                _updates.Add(update);
            }
        }
    }

    public void SolvePart1()
    {
        int sum = 0;
        
        foreach (List<int> update in _updates)
        {
            if (IsUpdateValid(update))
            {
                sum += update[update.Count / 2];
            }
        }
        
        _part1Answer = sum.ToString();
    }

    private bool IsUpdateValid(List<int> update)
    {
        bool isValid = true;
        
        for (int i = 0; i < update.Count; i++)
        {
            if (isValid is false)
            {
                return false;
            }
            
            if (_rules.ContainsKey(update[i]) is false)
            {
                continue;
            }
            
            HashSet<int> rule = _rules[update[i]];
            foreach (int value in rule)
            {
                int indexOfValue = update.IndexOf(value);
                if (indexOfValue != -1 && indexOfValue < i)
                {
                    isValid = false;
                }
            }
        }
        
        return isValid;
    }

    public void SolvePart2()
    {
        int sum = 0;
        
        foreach (List<int> update in _updates)
        {
            if (IsUpdateValid(update) is false)
            {
                List<int> correctedUpdate = CorrectedUpdate(update);
                
                sum += correctedUpdate[correctedUpdate.Count / 2];
            }
        }
        
        _part2Answer = sum.ToString();
    }

    private List<int> CorrectedUpdate(List<int> update)
    {
        List<int> correctedUpdate = [..update];

        bool shuffled = true;
        while (shuffled)
        {
            shuffled = false;
            
            for (int i = 0; i < correctedUpdate.Count; i++)
            {
                if (_rules.ContainsKey(correctedUpdate[i]) is false)
                {
                    continue;
                }
            
                HashSet<int> rule = _rules[correctedUpdate[i]];
                foreach (int value in rule)
                {
                    int indexOfValue = correctedUpdate.IndexOf(value);
                    if (indexOfValue != -1 && indexOfValue < i)
                    {
                        (correctedUpdate[i], correctedUpdate[indexOfValue]) = (correctedUpdate[indexOfValue], correctedUpdate[i]);
                        shuffled = true;
                        break;
                    }
                }
            }
        }
        
        return correctedUpdate;
    }

    public void PrintAnswer()
    {
        Console.WriteLine(_part1Answer);
        Console.WriteLine(_part2Answer);
    }
}