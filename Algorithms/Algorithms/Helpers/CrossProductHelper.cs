using System.Collections.Generic;
using System.Linq;
using Algorithms.Entities;

namespace Algorithms.Helpers
{
	public class CrossProductHelper
	{
		// Returns all pairs with one item from the first list and one item from
		// the second list.  (Cartesian product of the two lists.)
		public static List<Location> CrossProduct(IEnumerable<int> rows, IEnumerable<int> cols)
		{
			return (
				from r in rows
					from c in cols.ToList()
					select new Location(r, c)).ToList();
		}
	}
}