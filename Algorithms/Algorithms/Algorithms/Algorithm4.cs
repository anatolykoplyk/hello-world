using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Algorithms.Entities;
using Algorithms.Helpers;

namespace Algorithms.Algorithms
{
	class Algorithm4 : IPeakFinderAlgorithm
	{
		public Location FindPeak(PeakProblem problem, Logger logger, Location currentLocation = null, Location bestSeen = null, bool splitRows = false)
		{
			if (problem.NumRow <= 0 || problem.NumCol <= 0)
			{
				logger.AddMessage("Empty input data. No Peak");
				return null;
			}

			var subproblems = new List<Bound>();
			List<Location> divider;

			//the recursive subproblem will involve half the number of rows
			if (splitRows)
			{
				var mid = problem.NumRow/2;
				//information about the two subproblems
				var subStartR1 = 0;
				var subNumR1 = mid;
				var subStartR2 = mid + 1;
				var subNumR2 = problem.NumRow - (mid + 1);
				var subStartC = 0;
				var subNumC = problem.NumCol;

				subproblems.Add(new Bound(subStartR1, subStartC, subNumR1, subNumC));
				subproblems.Add(new Bound(subStartR2, subStartC, subNumR2, subNumC));
			
				//get a list of all locations in the dividing column
				divider = CrossProductHelper.CrossProduct(new[] {mid}, Enumerable.Range(0, problem.NumCol));
			}
			else
			{
				//the recursive subproblem will involve half the number of columns
				var mid = problem.NumCol/2;

				//information about the two subproblems
				var subStartR = 0; var subNumR = 0;
				var subStartC1 = 0; var subNumC1 = mid;
				var subStartC2 = mid + 1; var subNumC2 = problem.NumCol - (mid + 1);

				subproblems.Add(new Bound(subStartR, subStartC1, subNumR, subNumC1));
				subproblems.Add(new Bound(subStartR, subStartC2, subNumR, subNumC2));

				//# get a list of all locations in the dividing column
				divider = CrossProductHelper.CrossProduct(Enumerable.Range(0, problem.NumRow), new[] {mid});
			}

			//# find the maximum in the dividing row or column
			var bestLoc = problem.GetMaximum(divider, logger);
			var neighbor = problem.GetBetterNeighbor(bestLoc);

			//update the best we've seen so far based on this new maximum

			if (bestSeen == null || problem.GetValue(neighbor) > problem.GetValue(bestSeen))
			{
				bestSeen = neighbor;
			}

			//return when we know we've found a peak
			if (neighbor.Equals(bestLoc) && problem.GetValue(bestLoc) >= problem.GetValue(bestSeen))
			{
				return bestLoc;
			}

			//figure out which subproblem contains the largest number we've seen so
			//far, and recurse, alternating between splitting on rows and splitting
			//on columns
			var sub = problem.GetSubproblemContaining(subproblems, bestSeen);
			var newBest = sub.GetLocationInSelf(problem, bestSeen);
			var result = FindPeak(sub, logger, null, newBest, !splitRows);

			return problem.GetLocationInSelf(sub, result);
		}
	}
}
