using System;
using System.Collections.Generic;
using Algorithms.Entities;
using Algorithms.Helpers;

namespace Algorithms.Algorithms
{
	public static class Algorithm1
	{
		public static Location FindPeak(PeakProblem problem)
		{
			// if it's empty, we're done 
			if (problem.NumRow <= 0 || problem.NumCol <= 0)
			{
				Console.WriteLine("Empty input data. No Peak");
				return null;
			}

			// the recursive subproblem will involve half the number of columns
			var mid = problem.NumCol/2;

			// information about the two subproblems
			var subStartRow = new Tuple<int, int>(0, problem.NumRow);
			var subProblem1 = new Tuple<int, int>(0, mid);
			var subProblem2 = new Tuple<int, int>(mid + 1, problem.NumCol - (mid + 1));

			var subProblems = new List<Bound>
			{
				new Bound(
					subStartRow.Item1, //start row
					subProblem1.Item1, //start col
					subStartRow.Item2, // number of rows
					subProblem1.Item2), // number of cols
				new Bound(
					subStartRow.Item1,
					subProblem2.Item1,
					subStartRow.Item2,
					subProblem2.Item2)
			};
			
			// get a list of all locations in the dividing column
			var divider = CrossProduct(problem.NumRow, mid);

			// find the maximum in the dividing column
			var bestLoc = problem.GetMaximum(divider);

			// see if the maximum value we found on the dividing line has a better
			// neighbor (which cannot be on the dividing line, because we know that
			// this location is the best on the dividing line)
			var neighbor = problem.GetBetterNeighbor(bestLoc);

			// this is a peak, so return it
			if (neighbor.Equals(bestLoc))
			{
				//	if not trace is None: trace.foundPeak(bestLoc)
				return bestLoc;
			}

			// otherwise, figure out which subproblem contains the neighbor, and
			// recurse in that half
			var subProblem = problem.GetSubproblemContaining(subProblems, neighbor);

			//if not trace is None: trace.setProblemDimensions(sub)

			var result = FindPeak(subProblem);

			return problem.GetLocationInSelf(subProblem, result);
		}


		// Returns all pairs with one item from the first list and one item from
		// the second list.  (Cartesian product of the two lists.)
		private static IEnumerable<Location> CrossProduct(int rows, int cols)
		{
			var res = new List<Location>();

			for (int r = 0; r < rows; r++)
			{
				for (int c = 0; c < cols; c++)
				{
					res.Add(new Location(r, c));
				}
			}
			return res;
		}
	}
}
