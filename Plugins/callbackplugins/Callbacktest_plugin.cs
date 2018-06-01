// Copyright (c) 2014-2018, Els_kom org.
// https://github.com/Elskom/
// All rights reserved.
// license: MIT, see LICENSE for more details.

namespace callbacktest_plugin
{
    public class Callbacktest_plugin : Els_kom_Core.interfaces.ICallbackPlugin
    {
        public string PluginName => "Callback Test Plugin";
        public bool SupportsSettings => true;
        public System.Windows.Forms.Form SettingsWindow => new CallbacktestForm();

        public void TestModsCallback()
        {
            Els_kom_Core.Classes.SettingsFile.Settingsxml.ReopenFile();
            int.TryParse(Els_kom_Core.Classes.SettingsFile.Settingsxml.Read("ShowTestMessages"), out int callbacksetting1);
            if (callbacksetting1 > 0)
            {
                System.Windows.Forms.MessageBox.Show("Testing this callback interface.", "Info!", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
            }
        }
    }
}
