using System.Diagnostics;
using System.Linq;

namespace Els_kom_Core
{
	namespace Classes
	{
		/// <summary>
		/// Hosts a method that Overloads the Shell() Function. This is for Overloading the WorkingDirectory Method that bypasses some issues in x2.exe when shelling it. It Also Holds means of Detecting if the Launcher or x2.exe is currently Running.
		/// </summary>
		public static class Process
		{
			/// <summary>
			/// Overload for Shell() Function that Allows Overloading of the Working directory Variable. It must be a String but can be variables that returns strings.
			/// </summary>
			/// <returns>0</returns>
			public static object Shell(string FileName, string Arguments, bool RedirectStandardOutput, bool UseShellExecute, bool CreateNoWindow, ProcessWindowStyle WindowStyle, string WorkingDirectory, bool WaitForProcessExit)
			{
				System.Diagnostics.Process proc = new System.Diagnostics.Process();
				proc.StartInfo.FileName = FileName;
				proc.StartInfo.Arguments = Arguments;
				proc.StartInfo.RedirectStandardOutput = RedirectStandardOutput;
				proc.StartInfo.UseShellExecute = UseShellExecute;
				proc.StartInfo.CreateNoWindow = CreateNoWindow;
				proc.StartInfo.WindowStyle = WindowStyle;
				proc.StartInfo.WorkingDirectory = WorkingDirectory;
				proc.Start();
				if (WaitForProcessExit)
				{
					proc.WaitForExit();
				}
				// Required to have Detection on the process running to work right.
				return 0;
			}

			/// <summary>
			/// Gets if x2.exe is Running or Not.
			/// </summary>
			/// <returns>Boolean</returns>
			public static bool IsX2Running()
			{
				System.Diagnostics.Process[] x2exe = System.Diagnostics.Process.GetProcessesByName("Test_Mods");
				// Only Way for Detection to Work Right sadly.
				return x2exe.Any();
			}

			/// <summary>
			/// Gets if elsword.exe or voidels.exe is Running or Not.
			/// </summary>
			/// <returns>Boolean</returns>
			public static bool IsLauncherRunning()
			{
				System.Diagnostics.Process[] elswordexe = System.Diagnostics.Process.GetProcessesByName("elsword");
				System.Diagnostics.Process[] voidelsexe = System.Diagnostics.Process.GetProcessesByName("voidels");
				if (elswordexe.Any()) {
					return true;
				} else {
					//Due to support is needed for detecting the Void elsword Launcher it has to be here before returning False
					// to get if it is Void's Launcher being run instead of the official one.
					return voidelsexe.Any();
				}
			}

			/// <summary>
			/// Bypasses Elsword's Integrity Check (makes it read checkkom.xml locally).
			/// </summary>
			public static bool BypassIntegrityChecks()
			{
				// TODO: Add code that can bypass Integrity Checks without needing Fidler as GameGuard detects it.
				return true;
			}

			/// <summary>
			/// Gets if Els_kom.exe is already Running. If So, Helps with Closing any new Instances.
			/// </summary>
			/// <returns>Boolean</returns>
			public static bool IsElsKomRunning()
			{
				System.Diagnostics.Process[] els_komexe = System.Diagnostics.Process.GetProcessesByName("Els_kom");
				return els_komexe.Count() > 1;
			}
		}
	}
}