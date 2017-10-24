using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Els_kom_Core.Classes
{
    /// <summary>
    /// Class in the Core that allows executing Elsword directly or it's launcher.
    /// </summary>
    public static class ExecutionManager
    {
        public static string ElsDir;
        public static bool RunningElsword = false;
        public static bool RunningElswordDirectly = false;

        public static void RunElswordDirectly()
        {
            if (System.IO.File.Exists(Application.StartupPath + "\\Settings.ini"))
            {
                INIObject settingsini = new INIObject(Application.StartupPath + "\\Settings.ini");
                ElsDir = settingsini.Read("Settings.ini", "ElsDir");
                if (ElsDir.Length > 0)
                {
                    if (System.IO.File.Exists(ElsDir + "\\data\\x2.exe"))
                    {
                        RunningElswordDirectly = true;
                        Process.Shell(ElsDir + "\\data\\x2.exe", "pxk19slammsu286nfha02kpqnf729ck", false, false, false, System.Diagnostics.ProcessWindowStyle.Normal, ElsDir + "\\data\\", true);
                        RunningElswordDirectly = false;
                    }
                    else
                    {
                        MessageBox.Show("Can't find '" + ElsDir + "\\data\\x2.exe'. Make sure the File Exists and try to Test your mods Again!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("The Elsword Directory Setting is not set. Make sure to Set your Elsword Directory Setting and try to Test your mods Again!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("The Elsword Directory Setting is not set. Make sure to Set your Elsword Directory Setting and try to Test your mods Again!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static void RunElswordLauncher()
        {
            if (System.IO.File.Exists(Application.StartupPath + "\\Settings.ini"))
            {
                INIObject settingsini = new INIObject(Application.StartupPath + "\\Settings.ini");
                ElsDir = settingsini.Read("Settings.ini", "ElsDir");
                if (ElsDir.Length > 0)
                {
                    if (System.IO.File.Exists(ElsDir + "\\voidels.exe"))
                    {
                        RunningElsword = true;
                        Process.Shell(ElsDir + "\\voidels.exe", "", false, false, false, System.Diagnostics.ProcessWindowStyle.Normal, ElsDir, true);
                        RunningElsword = false;
                    }
                    else
                    {
                        if (System.IO.File.Exists(ElsDir + "\\elsword.exe"))
                        {
                            RunningElsword = true;
                            Process.Shell(ElsDir + "\\elsword.exe", "", false, false, false, System.Diagnostics.ProcessWindowStyle.Normal, ElsDir, true);
                            RunningElsword = false;
                        }
                        else
                        {
                            MessageBox.Show("Can't find '" + ElsDir + "\\elsword.exe'. Make sure the File Exists and try to update Elsword Again!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("The Elsword Directory Setting is not set. Make sure to Set your Elsword Directory Setting and try to update Elsword Again!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("The Elsword Directory Setting is not set. Make sure to Set your Elsword Directory Setting and try to update Elsword Again!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
