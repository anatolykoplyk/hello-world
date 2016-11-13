using Algorithms.Entities;
using Algorithms.Helpers;

namespace Algorithms.Algorithms
{
	interface IPeakFinderAlgorithm
	{
		Location FindPeak(PeakProblem problem, Logger logger, Location currentLocation = null, Location bestSeen = null, bool splitRows = false);
	}
}
