using System.Collections.Generic;
using Algorithms.Entities;

namespace Algorithms.Algorithms
{
	public static class Algorithm1
	{
		public static Location FindAPeak(PeakProblem problem)
		{
			//if it's empty, we're done 
			if (problem.NumRow <= 0 || problem.NumCol <= 0)
				return null;

			//# the recursive subproblem will involve half the number of columns
			var mid = problem.NumCol/2;

			var startRow = new Location(0, problem.NumRow);

			var subProblems = new List<PeakProblem>();
			subProblems.Add(new PeakProblem());
			//# information about the two subproblems
			//(subStartR, subNumR) = (0, problem.numRow)
			//(subStartC1, subNumC1) = (0, mid)
			//(subStartC2, subNumC2) = (mid + 1, problem.numCol - (mid + 1))

			//subproblems = []
			//subproblems.append((subStartR, subStartC1, subNumR, subNumC1))
			//subproblems.append((subStartR, subStartC2, subNumR, subNumC2))

			//# get a list of all locations in the dividing column
			//divider = crossProduct(range(problem.numRow), [mid])

			//# find the maximum in the dividing column
			//bestLoc = problem.getMaximum(divider, trace)

			//# see if the maximum value we found on the dividing line has a better
			//# neighbor (which cannot be on the dividing line, because we know that
			//# this location is the best on the dividing line)
			//neighbor = problem.getBetterNeighbor(bestLoc, trace)

			//# this is a peak, so return it
			//if neighbor == bestLoc:
			//	if not trace is None: trace.foundPeak(bestLoc)
			//	return bestLoc

			//# otherwise, figure out which subproblem contains the neighbor, and
			//# recurse in that half
			//sub = problem.getSubproblemContaining(subproblems, neighbor)
			//if not trace is None: trace.setProblemDimensions(sub)
			//result = algorithm1(sub, trace)
			//return problem.getLocationInSelf(sub, result)

			return null;
		}
	}
}
