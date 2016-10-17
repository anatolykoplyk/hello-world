using System;
using Algorithms.Helpers;
using System.Configuration;
using System.IO;
using Algorithms.Algorithms;
using Algorithms.Entities;

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
			var existingProblem = ConfigurationManager.AppSettings["problemFile"];

			var logger = new Logger(string.Format(ConfigurationManager.AppSettings["logFile"], DateTime.Now));

			if (!File.Exists(existingProblem))
			{
				logger.AddMessage("Generated new file.");

				PeakProblemGenerator.GenerateProblem(cols, rows, maxEl, 
					string.IsNullOrEmpty(existingProblem) ? string.Empty : existingProblem);
			}

			Console.WriteLine("File already exists. Programm will be working with the existing problem");

			logger.AddMessage("Working with existing file.");

			var peakProblem = PeakProblemGenerator.LoadProblemFromFile(existingProblem);

			var peak = Algorithm1.FindPeak(peakProblem, logger);
			
			var status = "is NOT a peak (INCORRECT!)";

			if (peakProblem.IsPeak(peak))
			{
				status = " is a peak";
			}

			logger.AddMessage("Algorithm1 : " + peak + status);
		}
	}
}
