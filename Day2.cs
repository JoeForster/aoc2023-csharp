using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace aoc2023_csharp
{
	internal class Day2 : IDaySolution
	{
		enum CubeColour { red, green, blue }
		
		private CubeColour ParseCubeColour(string input)
		{
			CubeColour colour = CubeColour.red;
			switch (input)
			{
			case "red":
				colour = CubeColour.red;
				break;
			case "green":
				colour = CubeColour.green;
				break;
			case "blue":
				colour = CubeColour.blue;
				break;
			default:
				Debug.Assert(false, "Unidentified colour: " + input);
				break;
			}
			return colour;
		}

		public void Part1(IEnumerable<string> inputLines)
		{
			int possibleIDSum = 0;
			foreach (string line in inputLines)
			{
				var cubeCounts = new Dictionary<CubeColour, int>
				{
					[CubeColour.red] = 12,
					[CubeColour.green] = 13,
					[CubeColour.blue] = 14
				};

				string[] sections = line.Split(":");
				Debug.Assert(sections.Length == 2);
				string gameLabel = sections[0];
				string[] gameAndID = gameLabel.Split(" ");
				Debug.Assert(gameAndID.Length == 2 && gameAndID[0] == "Game");
				int gameID = int.Parse(gameAndID[1]);
				
				bool possible = true;
				
				string[] picks = sections[1].Split(";");
				foreach (string pick in picks)
				{
					string[] colourCounts = pick.Split(",");
					foreach (string colourCount in colourCounts)
					{
						string[] countAndColour = colourCount.Trim().Split(" ");
						Debug.Assert(countAndColour.Length == 2);
						int count = int.Parse(countAndColour[0]);
						CubeColour colour = ParseCubeColour(countAndColour[1]);
						if (count > cubeCounts[colour])
						{
							possible = false;
						}
					}
				}

				if (possible)
				{
					possibleIDSum += gameID;
				}
			}
			
			Console.WriteLine("Sum of possible Game IDs: " + possibleIDSum);
			
		}

		public void Part2(IEnumerable<string> inputLines)
		{
			int powerSums = 0;
			foreach (string line in inputLines)
			{

				string[] sections = line.Split(":");
				Debug.Assert(sections.Length == 2);
				string gameLabel = sections[0];
				string[] gameAndID = gameLabel.Split(" ");
				Debug.Assert(gameAndID.Length == 2 && gameAndID[0] == "Game");
				int gameID = int.Parse(gameAndID[1]);

				var coloursMinimumNeeded = new Dictionary<CubeColour, int>();
				int coloursPower = 1;


				string[] picks = sections[1].Split(";");
				foreach (string pick in picks)
				{
					string[] colourCounts = pick.Split(",");
					foreach (string colourCount in colourCounts)
					{
						string[] countAndColour = colourCount.Trim().Split(" ");
						Debug.Assert(countAndColour.Length == 2);
						int count = int.Parse(countAndColour[0]);
						CubeColour colour = ParseCubeColour(countAndColour[1]);

						if (coloursMinimumNeeded.GetValueOrDefault(colour, 0) < count)
						{
							coloursMinimumNeeded[colour] = count;
						}
					}
				}
				
				foreach (int colourMinimum in coloursMinimumNeeded.Values)
				{
					coloursPower = coloursPower * colourMinimum;
				}				
				powerSums += coloursPower;
			}
			Console.WriteLine("Sum of all colour powers: " + powerSums);
		}
	}
}
