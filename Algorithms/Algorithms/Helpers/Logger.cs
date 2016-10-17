using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Algorithms.Entities;

namespace Algorithms.Helpers
{
	public class Logger
	{
		private readonly string _logFile;
		public Logger(string file)
		{
			_logFile = file;
		}

		public void LogGetMaximum(IEnumerable<Location> arguments, Location maximum)
		{
			string line1 = "FindingMaximum: " + arguments.Select(x => string.Format("[{0},{1}]; ", x.Row, x.Col));
			string line2 = "FoundMaximum: " + string.Format("[{0}, {1}]", maximum.Row, maximum.Col);
			using (System.IO.StreamWriter file = new System.IO.StreamWriter(_logFile))
			{
				file.WriteLine(line1);
				file.WriteLine(line2);
				file.WriteLine("\r\n");
				file.Close();
			}
		}

		public void AddMessage(string message)
		{
			using (System.IO.StreamWriter file = new System.IO.StreamWriter(_logFile))
			{
				file.WriteLine(message);
				file.WriteLine("\r\n");
				file.Close();
			}
		}
	}
}
