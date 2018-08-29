// Copyright (c) 2014-2018, Els_kom org.
// https://github.com/Elskom/
// All rights reserved.
// license: MIT, see LICENSE for more details.

namespace Els_kom.Forms
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Windows.Forms;
    using Els_kom_Core.Classes;

    internal partial class SettingsForm : Form
    {
        private static string label1 = string.Empty;
        private List<Task> tasks;
        private Timer taskTmr;

        internal SettingsForm() => this.InitializeComponent();

        internal static string Label1
        {
            get
            {
                if (label1 == string.Empty)
                {
                    label1 = "0";
                }

                return label1;
            }

            private set => label1 = value;
        }

        private void OpenPluginSettings(Form plugsettingfrm, IWin32Window owner)
        {
            plugsettingfrm.ShowDialog(owner);
            if (!plugsettingfrm.IsDisposed)
            {
                plugsettingfrm.Dispose();
            }

            plugsettingfrm = null;
        }

        private void SettingsForm_Load(object sender, EventArgs e)
        {
            // This new member is to ensure the data from
            // the control load function does not try to get the data
            // from the Settings XMLObject in Designer mode
            // (which contains no instance there anyway) that causes
            // the Designer to fail to load the form the control is on.
            this.SettingsControl1.InitControl();
            Label1 = "1";
            this.tasks = new List<Task>();
            this.components = this.components ?? new System.ComponentModel.Container();
            this.taskTmr = new Timer(this.components)
            {
                Enabled = true,
                Interval = 1
            };
            this.taskTmr.Tick += new EventHandler(this.TaskTmr_Tick);
        }

        private void SettingsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Label1 = "0";
            this.SettingsControl1.SaveSettings();
        }

        private void SettingsControl1_OpenPluginsForm(object sender, EventArgs e)
        {
            var pluginsForm = new PluginsForm();
            pluginsForm.ShowDialog();
            pluginsForm.Dispose();
        }

        private void SettingsControl1_OpenPluginsSettings(object sender, EventArgs e)
        {
            for (var i = 0; i < this.SettingsControl1.ListView1.SelectedItems.Count; i++)
            {
                var selitem = this.SettingsControl1.ListView1.SelectedItems[i];
                foreach (var callbackplugin in ExecutionManager.Callbackplugins)
                {
                    if (callbackplugin.PluginName.Equals(selitem.Text))
                    {
                        var plugsettingfrm = callbackplugin.SettingsWindow;
                        plugsettingfrm.Icon = Icons.FormIcon;
                        var task = callbackplugin.ShowModal
                            ? Task.Factory.StartNew(
                                () => this.OpenPluginSettings(plugsettingfrm, this))
                            : Task.Factory.StartNew(
                                () => this.OpenPluginSettings(plugsettingfrm, null));
                        this.tasks.Add(task);
                    }
                }
            }
        }

        private void TaskTmr_Tick(object sender, EventArgs e)
        {
            foreach (var task in this.tasks)
            {
                if (task != null)
                {
                    if (task.IsCompleted || task.IsFaulted || task.IsCanceled)
                    {
                        task.Dispose();
                    }
                }
            }
        }
    }
}
