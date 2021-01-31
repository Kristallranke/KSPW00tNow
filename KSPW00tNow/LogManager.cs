using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KSPW00tNow
{
    class LogManager
	{
		private static bool levelDebug = false;

		static public bool LevelDebug
		{
			get { return levelDebug; }   // get method
			set { levelDebug = value; }  // set method
		}

		static public void Debug(String name, String message = "")
		{
			if (LevelDebug)
				UnityEngine.Debug.Log(CreateLogString(name, message));
		}

		static public void Error(String name, String message = "")
		{
			Log(name, message);
		}

		static public void Log(String name, String message = "")
		{
			UnityEngine.Debug.Log(CreateLogString(name, message));
		}

		static public void Log(System.IO.StreamWriter writer, String name, String message = "")
		{
			writer.WriteLine(CreateLogString(name, message));
		}

		static private String CreateLogString(String name, String message = "")
		{
			String result;
			if (message == "")
			{
				result = $"[KSPW00tNow] {name}";
			}
			else
			{
				result = $"[KSPW00tNow] {name}: {message}";
			}
			return result;
		}
	}
}
