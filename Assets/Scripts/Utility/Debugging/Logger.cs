using UnityEngine;
using System;
using System.Text;
using System.IO;

namespace Multiverse.Utility.Debugging
{
	public class Logger
	{
		public enum LogLevel : byte
		{
			None = 0,
			Info,
			Access,
			Warning,
			Error,
			Debug
		}

		public static void Log(string message)
		{
			Debug.Log (message);
		}

		public static void Log(string message, params object[] arguments)
		{
			string str = string.Format (message, arguments);
			Debug.Log (str);
		}

		public static void Log(LogLevel level, string module, string format, params object[] arguments)
		{
			string timestamp = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
			string header = string.Format("{0} | {1,-7} | {2,-15} | ", timestamp, level, module);
			string debugstr = string.Format (header + format, arguments);
			Debug.Log (debugstr);
		}
	}
}