using System;
using Algorithms.Helpers;
using System.Configuration;

namespace Algorithms
{
	class Program
	{
		static void Main(string[] args)
		{
			int rows, cols, maxEl;
			int.TryParse(ConfigurationManager.AppSettings["rows"], out rows);
			int.TryParse(ConfigurationManager.AppSettings["cols"], out cols);
			int.TryParse(ConfigurationManager.AppSettings["max"], out maxEl);
			var fname = ConfigurationManager.AppSettings["problemFile"];
			MatrixGenerator.GenerateProblem(
				cols, 
				rows, 
				maxEl, 
				String.IsNullOrEmpty(fname) ? String.Empty : fname);

		}
	}
}
