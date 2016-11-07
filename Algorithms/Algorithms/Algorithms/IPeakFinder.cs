using Algorithms.Entities;
using Algorithms.Helpers;

namespace Algorithms.Algorithms
{
	interface IPeakFinder
	{
		Location FindPeak(PeakProblem problem, Logger logger, Location currentLocation = null);
	}
}
