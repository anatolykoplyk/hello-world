using System;
using System.Collections.Generic;
using Algorithms.Helpers;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
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
			var fileName = ConfigurationManager.AppSettings["problemFile"];

			var logger = new Logger("log.txt");

			logger.AddMessage(String.Format(ConfigurationManager.AppSettings["logFile"], DateTime.Now) + "\r\n");

			if (Convert.ToBoolean(ConfigurationManager.AppSettings["overwriteEverytime"]))
			{
				var generated = PeakProblemGenerator.CreateRandomMatrix(cols, rows, maxEl);
				var formattedArray = PeakProblemGenerator.FormatArray(generated).ToList();
				logger.AddMessage("Generated a new peak problem file.");
				File.WriteAllLines(fileName, formattedArray, Encoding.UTF8);
			}
			else
			{
				logger.AddMessage("File already exists. Programm will work using an existing problem file");
				logger.AddMessage("Working with existing file.");
			}
			
			var peakProblem = PeakProblemGenerator.LoadProblemFromFile(fileName);
			logger.AddMessage("Given problem:");
			logger.AddMessage(File.ReadAllLines(fileName).Aggregate((current, next) => current + "\r\n" + next));
			

			var algrthms = new List<IPeakFinderAlgorithm>(4)
			{
				new Algorithm1(),
				new Algorithm2(),
				new Algorithm3(),
				new Algorithm4()
			};

			foreach (var a in algrthms)
			{
				var algoName = a.GetType().Name;
				logger.AddMessage(string.Format("{0} started:", algoName));
				var peak = a.FindPeak(peakProblem, logger, new Location(0, 0));
				logger.AddMessage("Validating obtained peak...");
				var status = " is NOT a peak (INCORRECT!)";
				if (peakProblem.IsPeak(peak))
				{
					status = " is a peak";
				}
				logger.AddMessage(string.Format("{0} : {1} {2}\n\r", algoName, peak, status));
			}

			logger.AddMessage("\r\nFinished.");
			Console.WriteLine("Finished.");
		}
	}
}
