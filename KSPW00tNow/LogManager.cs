using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KSPW00tNow
{
    class LogManager
	{
		static bool enabled = false;

		static public void Log(String name, String message = "")
		{
			if(enabled)
				UnityEngine.Debug.Log(CreateLogString(name, message));
		}

		static public void Log(System.IO.StreamWriter writer, String name, String message = "")
		{
			if(enabled)
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
