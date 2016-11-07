using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Algorithms.Entities;

namespace Algorithms.Helpers
{
	class PeakProblemGenerator
	{
		public static IEnumerable<int[]> CreateRandomMatrix(int cols, int rows, int maxElement)
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

		public static void PrintProblem(IEnumerable<string> formatted)
		{
			foreach (var v in formatted)
			{
				Console.WriteLine(v);
			}
		}

		public static PeakProblem LoadProblemFromFile(string file)
		{
			var array = File.ReadAllLines(file);
			var dimensions = GetDimensions(array);
			return new PeakProblem(
				array.Select(i => Array.ConvertAll(i.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries), int.Parse)),
				new Bound(0, 0, dimensions.Item1, dimensions.Item2));
		}

		//Gets the dimensions for a two-dimensional array.The first dimension
		//is simply the number of items in the list; the second dimension is the
		//length of the shortest row.This ensures that any location (row, col)
		//that is less than the resulting bounds will in fact map to a valid
		//location in the array.
		private static Tuple<int, int> GetDimensions(string[] array)
		{
			var rows = array.Length;
			var cols = 0;

			foreach (var row in array)
			{
				var rowLenght = row.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries).Length;
				if (rowLenght > cols)
				{
					cols = rowLenght;
				}
			}
			return new Tuple<int, int>(rows, cols);
		}

		public static IEnumerable<string> FormatArray(IEnumerable<int[]> generated)
		{
			return generated.Select(x => string.Join("; ", x.Select(i => i.ToString(" 0#")))).ToArray();
		}
	}
}
