namespace AdventOfCode.Days;

public interface ISolver
{
    void Setup(string[] input);
    void SolvePart1();
    void SolvePart2();
    void PrintAnswer();
}