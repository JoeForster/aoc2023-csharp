using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aoc2023_csharp
{
	internal interface IDaySolution
	{
		void Part1(IEnumerable<string> inputLines);
		void Part2(IEnumerable<string> inputLines);
	}
}
