namespace AdventOfCode.Utilities;

public static class InputLoader
{
    public static string[] LoadInput(string day)
    {
        string baseDirectory = AppContext.BaseDirectory;
        string projectRoot = Path.GetFullPath(Path.Combine(baseDirectory, @"..\..\..\"));
        string inputPath = Path.Combine(projectRoot, "Inputs", $"Day{day}.txt");

        if (File.Exists(inputPath) is false)
        {
            throw new FileNotFoundException($"Input file not found at {inputPath}");
        }
        
        return File.ReadAllLines(inputPath);
    }
}