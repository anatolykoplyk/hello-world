using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms.Helpers
{
	class MatrixGenerator
	{


		private int[][] CreateRandomProblem(int cols = 10, int rows = 10, int maxElement = 1000)
		{
			var rnd = new Random();
			var result = new int[][] {};

			for (var m = 0; m < cols; m++)
			{
				var row = new int[] {};
				for (var n = 0; n < rows; n++)
				{
					row[n] = rnd.Next(0, maxElement);
				}
				result[m] = row;
			}

			return result;
		}
	}
}
