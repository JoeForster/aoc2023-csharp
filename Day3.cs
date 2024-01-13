using System.Diagnostics;

namespace aoc2023_csharp
{
	internal class Day3 : IDaySolution
	{
		// Enumerator function that scans for adjacent numbers (which could span multiple cells!)
		// In part one used just to sum, then refactored to share for part 2
		private IEnumerable<int> ScanForAdjacentNumbers(string[] lines, int symbolX, int symbolY)
		{
			// Scan the rows above and below for adjacent numbers
			foreach (int checkY in new int[]{symbolY - 1, symbolY + 1})
			{
				if (checkY >= 0 && checkY < lines.Length)
				{
					int lastNumberStartX = -1;
					string checkLine = lines[checkY];
					// Scan for numbers in the whole row, as they could start anywhere from 0 or finish anywhere up to the end
					// Could be more efficient here for ludicrously long rows by limiting where we scan, but this is fine for our inputs.
					for (int x = 0; x <= checkLine.Length; x++)
					{
						char charAtXY = (x == checkLine.Length) ? '.' : checkLine[x];
						int charAsInt = (int)char.GetNumericValue(charAtXY);
						if (charAsInt >= 0)
						{
							if (lastNumberStartX == -1)
							{
								lastNumberStartX = x;
							}
						}
						else if (lastNumberStartX != -1)
						{
							int lastNumberEndX = x-1;
							Debug.Assert(lastNumberStartX >= 0);
							bool isNumberAdjacent = lastNumberStartX <= symbolX+1 && lastNumberEndX >= symbolX-1;
							if (isNumberAdjacent)
							{
								string numberStr = checkLine.Substring(lastNumberStartX, x-lastNumberStartX);
								yield return int.Parse(numberStr);
							}
							lastNumberStartX = -1;
						}
					}
				}
			}

			// Scan for a number to the left of the symbol
			{
				int checkX = symbolX - 1;
				if (checkX >= 0)
				{
					string checkLine = lines[symbolY];
					char checkSymbol = checkLine[checkX];
					if ((int)char.GetNumericValue(checkSymbol) >= 0)
					{
						int numberStartX = checkX;
						while (numberStartX > 0 && (int)char.GetNumericValue(checkLine[numberStartX-1]) >= 0)
						{
							numberStartX = numberStartX - 1;
						}
					
						string numberStr = checkLine.Substring(numberStartX, symbolX-numberStartX);
						yield return int.Parse(numberStr);
					}
				}
			}

			// Scan for a number to the right of the symbol
			{
				string checkLine = lines[symbolY];
				int checkX = symbolX + 1;
				if (checkX < checkLine.Length)
				{
					char checkSymbol = checkLine[checkX];
					if ((int)char.GetNumericValue(checkSymbol) >= 0)
					{
						int numberEndX = checkX;
						while (numberEndX+1 < checkLine.Length && (int)char.GetNumericValue(checkLine[numberEndX+1]) >= 0)
						{
							numberEndX = numberEndX + 1;
						}
					
						string numberStr = checkLine.Substring(checkX, numberEndX-symbolX);
						yield return int.Parse(numberStr);
					}
				}
			}
		}

		public void Part1(IEnumerable<string> inputLines)
		{
			int total = 0;

			string[] lines = inputLines.ToArray();
			
			for (int y = 0; y < lines.Length; y++)
			{
				string line = lines[y];
				for (int x = 0; x < line.Length; x++)
				{
					char charAtXY = line[x];
					if (charAtXY == '.')
					{
						continue;
					}
					else
					{
						int charAsInt = (int)char.GetNumericValue(charAtXY);
						if (charAsInt < 0)
						{
							IEnumerable<int> adjacents = ScanForAdjacentNumbers(lines, x, y);
							int adjacentSum = adjacents.Sum();
							Console.WriteLine(String.Format("Total for symbol at {0},{1}: {2}", x, y, adjacentSum));
							total += adjacentSum;
						}
					}
				}
			}
			
			Console.WriteLine("Sum of numbers adjacent to symbols: " + total);
		}

		public void Part2(IEnumerable<string> inputLines)
		{
			int total = 0;

			string[] lines = inputLines.ToArray();
			
			for (int y = 0; y < lines.Length; y++)
			{
				string line = lines[y];
				for (int x = 0; x < line.Length; x++)
				{
					char charAtXY = line[x];
					if (charAtXY == '.')
					{
						continue;
					}
					else
					{
						int charAsInt = (int)char.GetNumericValue(charAtXY);
						if (charAsInt < 0)
						{
							// Product only if there would be exactly two results (get two and try to get a third)
							IEnumerable<int> adjacents = ScanForAdjacentNumbers(lines, x, y);
							int[] firstUpToThree = adjacents.Take(3).ToArray();
							if (firstUpToThree.Length == 2)
							{
								int product = firstUpToThree[0] * firstUpToThree[1];
								Console.WriteLine(String.Format("Symbol at {0},{1} has TWO values {2} and {3} -> product is {4}", x, y, firstUpToThree[0], firstUpToThree[1], product));
								total += product;
							}
						}
					}
				}
			}
			
			Console.WriteLine("Sum of products for symbols with exactly two adjacent numbers: " + total);
		}
	}
}
