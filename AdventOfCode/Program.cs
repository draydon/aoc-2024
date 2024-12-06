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
            Console.WriteLine("Invalid day.");
            return;
        }
        
        ISolver solver = dayNumber switch
        {
            1 => new Day1Solver(),
            2 => new Day2Solver(),
            3 => new Day3Solver(),
            4 => new Day4Solver(),
            5 => new Day5Solver(),
            _ => throw new Exception($"Invalid day {day}")
        };
        
        DateTime start = DateTime.Now;

        solver.Setup(InputLoader.LoadInput(day));
        solver.SolvePart1();
        solver.SolvePart2();
        solver.PrintAnswer();
        DateTime end = DateTime.Now;

        Console.WriteLine("Day {0} solved in {1}.", dayNumber, end - start);
    }
}