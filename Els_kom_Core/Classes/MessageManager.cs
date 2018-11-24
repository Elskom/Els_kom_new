// Copyright (c) 2014-2018, Els_kom org.
// https://github.com/Elskom/
// All rights reserved.
// license: MIT, see LICENSE for more details.

namespace Els_kom_Core.Classes
{
    using System;
    using System.Windows.Forms;

    /// <summary>
    /// Els_kom's Generic MessageBox Manager.
    /// </summary>
    internal static class MessageManager
    {
        /// <summary>
        /// Shows an MessageBox that is for an Error.
        /// </summary>
        /// <param name="text">The text on the messagebox.</param>
        /// <param name="caption">The title of the messagebox.</param>
        /// <param name="notifyIcon">The notification icon to use if the program is set to use Notifications instead of message boxes.</param>
        /// <returns>A new <see cref="DialogResult"/>.</returns>
        internal static DialogResult ShowError(string text, string caption, NotifyIcon notifyIcon)
        {
            if (notifyIcon != null)
            {
                var notifications = Convert.ToBoolean(
                    Convert.ToInt32(
                        SettingsFile.Settingsxml?.TryRead("UseNotifications") != string.Empty ? SettingsFile.Settingsxml?.TryRead("UseNotifications") : "0"));
                if (notifications)
                {
                    notifyIcon.ShowBalloonTip(0, caption, text, ToolTipIcon.Error);
                    return DialogResult.OK;
                }

                return MessageBox.Show(text, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return MessageBox.Show(text, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        /// <summary>
        /// Shows an MessageBox that is for information.
        /// </summary>
        /// <param name="text">The text on the messagebox.</param>
        /// <param name="caption">The title of the messagebox.</param>
        /// <param name="notifyIcon">The notification icon to use if the program is set to use Notifications instead of message boxes.</param>
        /// <returns>A new <see cref="DialogResult"/>.</returns>
        internal static DialogResult ShowInfo(string text, string caption, NotifyIcon notifyIcon)
        {
            if (notifyIcon != null)
            {
                var notifications = Convert.ToBoolean(
                    Convert.ToInt32(
                        SettingsFile.Settingsxml?.TryRead("UseNotifications") != string.Empty ? SettingsFile.Settingsxml?.TryRead("UseNotifications") : "0"));
                if (notifications)
                {
                    notifyIcon.ShowBalloonTip(0, caption, text, ToolTipIcon.Info);
                    return DialogResult.OK;
                }

                return MessageBox.Show(text, caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            return MessageBox.Show(text, caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// Shows an MessageBox that is for an Warning.
        /// </summary>
        /// <param name="text">The text on the messagebox.</param>
        /// <param name="caption">The title of the messagebox.</param>
        /// <param name="notifyIcon">The notification icon to use if the program is set to use Notifications instead of message boxes.</param>
        /// <returns>A new <see cref="DialogResult"/>.</returns>
        internal static DialogResult ShowWarning(string text, string caption, NotifyIcon notifyIcon)
        {
            if (notifyIcon != null)
            {
                var notifications = Convert.ToBoolean(
                    Convert.ToInt32(
                        SettingsFile.Settingsxml?.TryRead("UseNotifications") != string.Empty ? SettingsFile.Settingsxml?.TryRead("UseNotifications") : "0"));
                if (notifications)
                {
                    notifyIcon.ShowBalloonTip(0, caption, text, ToolTipIcon.Warning);
                    return DialogResult.OK;
                }

                return MessageBox.Show(text, caption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            return MessageBox.Show(text, caption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        /// <summary>
        /// Shows an MessageBox that is for an Question.
        /// </summary>
        /// <param name="text">The text on the messagebox.</param>
        /// <param name="caption">The title of the messagebox.</param>
        /// <returns>A new <see cref="DialogResult"/>.</returns>
        internal static DialogResult ShowQuestion(string text, string caption)
            => MessageBox.Show(text, caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
    }
}
