using System;
using System.Windows.Forms;

namespace Els_kom_Core
{
	namespace Classes
	{
		/// <summary>
		/// Class in the Core that allows Packing and Unpacking kom Files.
		/// </summary>
		public static class KOMManager
		{
			/* required to see if we are packing or unpacking. Default to false. */
			public static bool is_packing = false;
			public static bool is_unpacking = false;

			public static void UnpackKoms()
			{
				is_unpacking = true;
				System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(Application.StartupPath + "\\koms");
				foreach (var fi in di.GetFiles("*.kom"))
				{
					string _kom_file = fi.Name;
					// remove ".kom" on end of string.
					string _kom_data_folder = System.IO.Path.GetFileNameWithoutExtension(Application.StartupPath + "\\koms\\" + _kom_file);
					Classes.Process.Shell(Application.StartupPath + "\\komextract_new.exe", "--in \"" + Application.StartupPath + "\\koms\\" + _kom_file + "\" --out \"" + Application.StartupPath + "\\koms\\" + _kom_data_folder + "\"", false, false, true, System.Diagnostics.ProcessWindowStyle.Hidden, Application.StartupPath, true);
					fi.Delete();
				}
				is_unpacking = false;
			}

			public static void PackKoms()
			{
				is_packing = true;
				System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(Application.StartupPath + "\\koms");
				foreach (var dri in di.GetDirectories())
				{
					string _kom_data_folder = dri.Name;
					string _kom_file = _kom_data_folder + ".kom";
					Classes.Process.Shell(Application.StartupPath + "\\kompact_new.exe", "--in \"" + Application.StartupPath + "\\koms\\" + _kom_data_folder + "\" --out \"" + Application.StartupPath + "\\koms\\" + _kom_file + "\"", false, false, true, System.Diagnostics.ProcessWindowStyle.Hidden, Application.StartupPath, true);
					foreach (var fi in dri.GetFiles()) {
						fi.Delete();
					}
					dri.Delete();
				}
				is_packing = false;
			}
		}
	}
}