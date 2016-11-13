using System.Collections.Generic;
using System.Linq;
using Algorithms.Entities;
using Algorithms.Helpers;

namespace Algorithms.Algorithms
{
	public class Algorithm3 : IPeakFinderAlgorithm
	{
		public Location FindPeak(PeakProblem problem, Logger logger, Location currentLocation = null, Location bestSeen = null, bool splitRows = false)
		{
			if (problem.NumRow <= 0 || problem.NumCol <= 0)
			{
				logger.AddMessage("Empty input data. No Peak");
				return null;
			}

			//first, get the list of all subproblems
			var midRow = problem.NumRow/2;
			var midCol = problem.NumCol/2;
			
			var subStartR1 = 0;
			var subStartR2 = midRow + 1;

			var subNumR1 = midRow;
			var subNumR2 = problem.NumRow - (midRow + 1);
			
			var subStartC1 = 0;
			var subStartC2 = midCol + 1;

			var subNumC1 = midRow;
			var subNumC2 = problem.NumRow - (midRow + 1);

			var subProblems = new List<Bound>
			{
				new Bound(subStartR1, subStartC1, subNumR1, subNumC1),
				new Bound(subStartR1, subStartC2, subNumR1, subNumC2),
				new Bound(subStartR2, subStartC1, subNumR2, subNumC1),
				new Bound(subStartR2, subStartC2, subNumR2, subNumC2)
			};

			//find the best location on the cross (the middle row combined with the middle column)

			var cross = CrossProductHelper.CrossProduct(new [] { midRow }, Enumerable.Range(0, problem.NumCol));
			cross.AddRange(CrossProductHelper.CrossProduct(Enumerable.Range(0, problem.NumRow) , new[] { midCol }));

			var crossLoc = problem.GetMaximum(cross, logger);
			var neighbor = problem.GetBetterNeighbor(crossLoc);


			//update the best we've seen so far based on this new maximum
			if (bestSeen == null || problem.GetValue(neighbor) > problem.GetValue(bestSeen))
			{
				bestSeen = neighbor;
			}

			//return if we can't see any better neighbors
			if (neighbor.Equals(crossLoc))
			{
				return crossLoc;
			}

			//figure out which subproblem contains the largest number we've seen so far, and recurse
			var sub = problem.GetSubproblemContaining(subProblems, bestSeen);

			var newBest = sub.GetLocationInSelf(problem, bestSeen);

			var result = FindPeak(sub, logger, null, newBest);

			return problem.GetLocationInSelf(sub, result);
		}
	}
}
