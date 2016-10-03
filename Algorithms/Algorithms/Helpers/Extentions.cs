using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Algorithms.Entities;

namespace Algorithms.Helpers
{
	public static class Extentions
	{
		public static PeakProblem GetSubproblemContaining(this PeakProblem self, IEnumerable<Bound> bounds, Location loc)
		{
			//Returns the subproblem containing the given location.  Picks the first
			//of the subproblems in the list which satisfies that constraint, and
			//then constructs the subproblem using getSubproblem().

			//RUNTIME: O(len(boundList))

			foreach (var b in bounds)
			{
				if (b.StartRow > loc.Row || loc.Row >= b.StartRow + b.NumRow) continue;

				if (b.StartCol <= loc.Col && loc.Col < b.StartCol + b.NumCol)
				{
					return self.GetSubproblem(b);
				}
			}

			return self;
		}
	}
}
