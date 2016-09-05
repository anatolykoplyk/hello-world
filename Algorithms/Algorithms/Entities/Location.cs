using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms.Entities
{
	public class Location : Tuple<int, int>
	{
		public Location(int row, int col) : base(row, col) { }

		public int Row { get { return Item1; } }
		public int Col { get { return Item2; } }
	}
}
