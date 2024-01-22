using System.Diagnostics;
using static System.Formats.Asn1.AsnWriter;

namespace aoc2023_csharp
{
	internal class Day4 : IDaySolution
	{
		public void Part1(IEnumerable<string> inputLines)
		{
			int totalScore = 0;
			foreach (string line in inputLines)
			{
				int colonPos = line.IndexOf(':');
				if (colonPos < 0)
				{
					continue;
				}
				string[] sections = line.Substring(colonPos+1).Split("|");
				if (sections.Length != 2)
				{
					continue;
				}

				// Probably not the most efficient, but hey
				int[] winningNumbers = sections[0].Trim().Split(new char[0], StringSplitOptions.RemoveEmptyEntries).Select(valueStr => int.Parse(valueStr)).ToArray();
				int[] myNumbers = sections[1].Trim().Split(new char[0], StringSplitOptions.RemoveEmptyEntries).Select(valueStr => int.Parse(valueStr)).ToArray();
				
				int score = 0;
				foreach (int myNumber in myNumbers)
				{
					if (winningNumbers.Contains(myNumber))
					{
						score = score == 0 ? 1 : score * 2;
					}
				}

				Console.WriteLine(String.Format("{0} score: {1}", line.Substring(0, colonPos), score));
				totalScore += score;
			}

			Console.WriteLine(String.Format("TOTAL score: {0}", totalScore));
		}

		public void Part2(IEnumerable<string> inputLines)
		{
			string[] originalLines = inputLines.ToArray();
			int numCards = originalLines.Length;
			Queue<int> unreadCards = new Queue<int>();
			for (int cardNo = 1; cardNo <= numCards; cardNo++)
			{
				unreadCards.Enqueue(cardNo);
			}

			int totalCards = numCards;

			while (unreadCards.Any())
			{
				int cardNo = unreadCards.Dequeue();
				string line = originalLines[cardNo-1];

				int colonPos = line.IndexOf(':');
				if (colonPos < 0)
				{
					continue;
				}
				string[] sections = line.Substring(colonPos+1).Split("|");
				if (sections.Length != 2)
				{
					continue;
				}
				
				string lineLabel = line.Substring(0, colonPos);
				int readCardNo = int.Parse( lineLabel.Substring(lineLabel.LastIndexOf(' ')+1) );
				Debug.Assert(readCardNo == cardNo); // ASSUMING the sequence of card numbers is consecutive and starts at 1!

				// Probably not the most efficient, but hey
				int[] winningNumbers = sections[0].Trim().Split(new char[0], StringSplitOptions.RemoveEmptyEntries).Select(valueStr => int.Parse(valueStr)).ToArray();
				int[] myNumbers = sections[1].Trim().Split(new char[0], StringSplitOptions.RemoveEmptyEntries).Select(valueStr => int.Parse(valueStr)).ToArray();
				
				int numMatches = 0;
				foreach (int myNumber in myNumbers)
				{
					if (winningNumbers.Contains(myNumber))
					{
						numMatches += 1;
					}
				}

				//Console.WriteLine(String.Format("Card {0} matches: {1}", cardNo, numMatches));
				
				for (int copyCardNo = cardNo + 1; copyCardNo <= cardNo+numMatches && copyCardNo <= numCards; copyCardNo++)
				{
					//Console.WriteLine(String.Format("  --> wins card {0}", copyCardNo));
					unreadCards.Enqueue(copyCardNo);
					totalCards++;
				}

			}

			Console.WriteLine(String.Format("TOTAL cards: {0}", totalCards));
		}
	}
}
