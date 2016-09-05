using System;
using System.Collections.Generic;
using System.Linq;

namespace Algorithms.Entities
{
	public class PeakProblem
	{
		private readonly int[][] _array;

		public PeakProblem(IEnumerable<int[]> array, Bounds bounds)
		{
			_array = array.ToArray();
			StartRow = bounds.StartRow;
			StartCol = bounds.StartCol;
			NumRow = bounds.NumRow;
			NumCol = bounds.NumCol;
		}
		
		public int StartRow { get; private set; }
		public int StartCol { get; private set; }
		public int NumRow { get; private set; }
		public int NumCol { get; private set; }

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
		public Location GetMaximum(IEnumerable<Location> locations)
		{
			Location bestLocation = null;
			var bestValue = 0;
			foreach (var loc in locations)
			{
				var v = GetLocationValue(loc);
				if (bestLocation == null ||  v > bestValue)
				{
					bestLocation = loc;
					bestValue = v;
				}
			}

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
		public PeakProblem GetSubproblem(Bounds bounds)
		{
			var newBounds = new Bounds(
				StartRow + bounds.StartRow, 
				StartCol + bounds.StartCol,
				bounds.NumRow,
				bounds.NumCol);
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

		/*

    def getSubproblemContaining(self, boundList, location):
        """
        Returns the subproblem containing the given location.  Picks the first
        of the subproblems in the list which satisfies that constraint, and
        then constructs the subproblem using getSubproblem().

        RUNTIME: O(len(boundList))
        """

        (row, col) = location
        for (sRow, sCol, nRow, nCol) in boundList:
            if sRow <= row and row < sRow + nRow:
                if sCol <= col and col < sCol + nCol:
                    return self.getSubproblem((sRow, sCol, nRow, nCol))

        # shouldn't reach here
        return self

    def getLocationInSelf(self, problem, location):
        """
        Remaps the location in the given problem to the same location in
        the problem that this function is being called from.

        RUNTIME: O(1)
        """

        (row, col) = location
        newRow = row + problem.startRow - self.startRow
        newCol = col + problem.startCol - self.startCol
        return (newRow, newCol)

################################################################################
################################ Helper Methods ################################
################################################################################

def getDimensions(array):
    """
    Gets the dimensions for a two-dimensional array.  The first dimension
    is simply the number of items in the list; the second dimension is the
    length of the shortest row.  This ensures that any location (row, col)
    that is less than the resulting bounds will in fact map to a valid
    location in the array.

    RUNTIME: O(len(array))
    """

    rows = len(array)
    cols = 0
    
    for row in array:
        if len(row) > cols:
            cols = len(row)
    
    return (rows, cols)

def createProblem(array):
    """
    Constructs an instance of the PeakProblem object for the given array,
    using bounds derived from the array using the getDimensions function.
   
    RUNTIME: O(len(array))
    """

    (rows, cols) = getDimensions(array)
    return PeakProblem(array, (0, 0, rows, cols))
		 */




	}
}
