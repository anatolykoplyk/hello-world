using Algorithms.Entities;
using Algorithms.Helpers;

namespace Algorithms.Algorithms
{
	public class Algorithm2 : IPeakFinderAlgorithm
	{
		public Location FindPeak(PeakProblem problem, Logger logger, Location currentLocation = null, Location bestSeen = null, bool splitRows = false)
		{
			if (problem.NumRow <= 0 || problem.NumCol <= 0)
			{
				logger.AddMessage("Empty input data. No Peak");
				return null;
			}

			var nextLocation = problem.GetBetterNeighbor(currentLocation);

			if(currentLocation != null && currentLocation.Equals(nextLocation))
			{
				return currentLocation;
			}
			return FindPeak(problem, logger, nextLocation);
		}
	}
}
