using System.IO;

namespace Algorithms.Helpers
{
	public class Logger
	{
		private readonly string _logFile;
		public Logger(string file)
		{
			_logFile = file;
		}
		
		public void AddMessage(string message)
		{
			using (StreamWriter sw = File.AppendText(_logFile))
			{
				sw.WriteLine(message);
				sw.Close();
			}
		}
	}
}
