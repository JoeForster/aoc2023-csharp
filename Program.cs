using aoc2023_csharp;

internal class Program
{
	private static IDaySolution GetDaySolution(int dayNumber)
	{
		IDaySolution result = null;

		// TODO: Could do something fancy with reflection here to get the type
		// at runtime and instantiate based on the passed dayNumber param.
		switch (dayNumber)
		{
			case 1:
				return new Day1();
			case 2:
				return new Day2();
			case 3:
				return new Day3();
			default:
				throw new ArgumentException("Invalid dayNumber passed");
		}
	}

	private static void Main(string[] args)
	{
		if (args.Length == 0)
		{
			Console.WriteLine("Expected a parameter for solution name (day number)");
			return;
		}

		string dayNumberStr = args[0];
		Console.WriteLine("Solution name passed: " + dayNumberStr);
		int dayNumber = int.Parse(dayNumberStr);
		IDaySolution solution = GetDaySolution(dayNumber);

		string test_file_name;
		if (args.Length > 1)
		{
			test_file_name = args[1];
		}
		else
		{
			test_file_name = String.Format("day{0}.txt", dayNumber);
		}
		var test_file_path = Path.Combine(Environment.CurrentDirectory, @"inputs\", test_file_name);
		Console.WriteLine("Read file: " + test_file_path);

		solution.Part1(File.ReadLines(test_file_path));
		solution.Part2(File.ReadLines(test_file_path));
	}
}