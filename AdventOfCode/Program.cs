using AdventOfCode.Days;
using AdventOfCode.Utilities;

namespace AdventOfCode;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Enter the day to solve:");
        string day = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(day) || int.TryParse(day, out int dayNumber) is false)
        {
            Console.WriteLine("Invalid dat.");
            return;
        }
        
        ISolver solver = dayNumber switch
        {
            1 => new Day1Solver(),
            2 => new Day2Solver(),
            _ => throw new Exception($"Invalid day {day}")
        };
        
        solver.Setup(InputLoader.LoadInput(day));
        solver.SolvePart1();
        solver.SolvePart2();
        solver.PrintAnswer();
    }
}