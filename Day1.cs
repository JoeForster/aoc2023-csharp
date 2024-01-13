using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace aoc2023_csharp
{
	internal class Day1 : IDaySolution
	{
		public void Part1(IEnumerable<string> inputLines)
		{
			int sum = 0;
			foreach (string line in inputLines)
			{
				int result = 0;
				Match firstMatch = Regex.Match(line, @"([0-9])");
				Match lastMatch = Regex.Match(line, @"([0-9])", RegexOptions.RightToLeft);
				if (firstMatch.Success && lastMatch.Success)
				{
					string concatenated = firstMatch.Value + lastMatch.Value;
					result = int.Parse(concatenated);
				}
				sum += result;
			}
	
			Console.WriteLine("Day1 A Result: " + sum);
		}

		public void Part2(IEnumerable<string> inputLines)
		{
			string[] numbers = {"one", "two", "three", "four", "five", "six", "seven", "eight", "nine"};
			string numbers_regex = String.Join("|", numbers);

			var toDecimalString = (string numberStr) =>
			{
				string decNum;
				int numIndex = Array.IndexOf(numbers, numberStr);
				if (numIndex >= 0)
				{
					decNum = (numIndex+1).ToString();
				}
				else
				{
					decNum = numberStr;
				}
				return decNum;
			};

			int sum = 0;
			foreach (string line in inputLines)
			{ 
				int result = 0;
				Match firstMatch = Regex.Match(line, @"([0-9]|"+numbers_regex+")");
				Match lastMatch = Regex.Match(line, @"([0-9]|"+numbers_regex+")", RegexOptions.RightToLeft);
				if (firstMatch.Success && lastMatch.Success)
				{
					string firstStr = toDecimalString(firstMatch.Value);
					string lastStr = toDecimalString(lastMatch.Value);

					string concatenated = firstStr + lastStr;

					//Console.WriteLine(
					//	String.Format("Line '{0}' -> first {1}={2} + last {3}={4} -> {5}",
					//		line, firstMatch.Value, firstStr, lastMatch.Value, lastStr, concatenated));
					result = int.Parse(concatenated);
				}
				sum += result;
			}
	
			Console.WriteLine("Day1 B Result: " + sum);
		}


	}
}
