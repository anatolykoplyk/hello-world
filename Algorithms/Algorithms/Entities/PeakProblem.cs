using System;
using System.Collections.Generic;
using System.Linq;
using Algorithms.Helpers;

namespace Algorithms.Entities
{
	public class PeakProblem
	{
		private readonly int[][] _array;

		public PeakProblem(IEnumerable<int[]> array, Bound bound)
		{
			_array = array.ToArray();
			StartRow = bound.StartRow;
			StartCol = bound.StartCol;
			NumRow = bound.NumRow;
			NumCol = bound.NumCol;
		}
		
		public int StartRow { get; }
		public int StartCol { get; }
		public int NumRow { get; }
		public int NumCol { get; }

		//Returns the value of the array at the given location, offset by
		//the coordinates(startRow, startCol).
		//RUNTIME: O(1)
		public int  GetLocationValue(Location location)
		{
			if (location.Row < 0 || location.Row >= NumRow)
			{
				return 0;
			}

			if (location.Col < 0 || location.Col >= NumCol)
			{
				return 0;
			}
			
			return _array[StartRow + location.Row][StartCol + location.Col];
		}


		//If current Location has a better neighbor, return the neighbor. Otherwise, return current.
		// RUNTIME: O(1)
		public Location GetBetterNeighbor(Location current)
		{
			var row = current.Row;
			var col = current.Col;
			var best = current;

			var next = new Location(row - 1, col);
			if (row - 1 >= 0 && GetLocationValue(next) > GetLocationValue(best))
			{
				best = next;
			}

			next = new Location(row, col - 1);
			if (col - 1 >= 0 && GetLocationValue(next) > GetLocationValue(best))
			{
				best = next;
			}

			next = new Location(row + 1, col);
			if (row + 1 < NumRow && GetLocationValue(next) > GetLocationValue(best))
			{
				best = next;
			}

			next = new Location(row, col + 1);
			if (col + 1 < NumCol && GetLocationValue(next) > GetLocationValue(best))
			{
				best = next;
			}

			return best;
		}

		//Finds the location in the current problem with the greatest value.
		//RUNTIME: O(len(locations))
		public Location GetMaximum(IEnumerable<Location> locations, Logger logger)
		{
			Location bestLocation = null;
			var bestValue = 0;
			var locationList = locations.ToList();
			foreach (var loc in locationList)
			{
				var v = GetLocationValue(loc);
				if (bestLocation == null ||  v > bestValue)
				{
					bestLocation = loc;
					bestValue = v;
				}
			}

			string s = "Found possible peak at: " + string.Format("Row={0}, Col={1}, Value={2}", bestLocation.Row, bestLocation.Col, bestValue);
			logger.AddMessage(s);

			return bestLocation;
		}

		//Returns true if the given location is a peak in the current subproblem.
		//RUNTIME: O(1)
		public bool IsPeak(Location loc)
		{
			return GetBetterNeighbor(loc).Equals(loc);
		}

		//Returns a subproblem with the given bounds.The bounds is a quadruple
		//of numbers: (starting row, starting column, # of rows, # of columns).
		//RUNTIME: O(1)
		public PeakProblem GetSubproblem(Bound bound)
		{
			var newBounds = new Bound(
				StartRow + bound.StartRow, 
				StartCol + bound.StartCol,
				bound.NumRow,
				bound.NumCol);
			return new PeakProblem(_array, newBounds);
		}
		
		//Remaps the location in the given problem to the same location in
		//the problem that this function is being called from.
		//RUNTIME: O(1)
		public Location GetLocationInSelf(PeakProblem problem, Location location)
		{
			var newRow = location.Row + problem.StartRow - StartRow;
			var newCol = location.Col + problem.StartCol - StartCol;

			return new Location(newRow, newCol);
		}
	}
}
