using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Algorithms.Helpers
{
	class MatrixGenerator
	{
		public static void GenerateProblem(int cols = 10, int rows = 10, int max = 1000, string fileName = "problem.txt" )
		{
			var generated = CreateRandomMatrix(cols, rows, max);
			Console.WriteLine("Generated a matrix with {0} row and {1} columns.", rows, cols);
			var formatted = generated.Select(x => string.Join(" ", x.Select(i => i.ToString(" 0#")))).ToArray();
			foreach (var v in formatted)
			{
				Console.WriteLine(v);
			}
			File.WriteAllLines(fileName, formatted, Encoding.UTF8);
		}

		private static IEnumerable<int[]> CreateRandomMatrix(int cols, int rows, int maxElement)
		{
			var rnd = new Random((int)DateTime.Now.Ticks);
			var array = new int[cols][];
			
			for (var i = 0; i < cols; i++)
			{
				var row = new List<int>(cols);
				for (var j = 0; j < rows; j++)
				{
					row.Add(rnd.Next(0, maxElement));
				}
				array[i] = row.ToArray();
			}

			return array;
		}
	}
}
