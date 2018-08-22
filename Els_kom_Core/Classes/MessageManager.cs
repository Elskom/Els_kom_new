// Copyright (c) 2014-2018, Els_kom org.
// https://github.com/Elskom/
// All rights reserved.
// license: MIT, see LICENSE for more details.

namespace Els_kom_Core.Classes
{
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
        /// <returns>A new <see cref="System.Windows.Forms.DialogResult"/>.</returns>
        internal static System.Windows.Forms.DialogResult ShowError(string text, string caption) => System.Windows.Forms.MessageBox.Show(text, caption, System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);

        /// <summary>
        /// Shows an MessageBox that is for information.
        /// </summary>
        /// <param name="text">The text on the messagebox.</param>
        /// <param name="caption">The title of the messagebox.</param>
        /// <returns>A new <see cref="System.Windows.Forms.DialogResult"/>.</returns>
        internal static System.Windows.Forms.DialogResult ShowInfo(string text, string caption) => System.Windows.Forms.MessageBox.Show(text, caption, System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);

        /// <summary>
        /// Shows an MessageBox that is for an Warning.
        /// </summary>
        /// <param name="text">The text on the messagebox.</param>
        /// <param name="caption">The title of the messagebox.</param>
        /// <returns>A new <see cref="System.Windows.Forms.DialogResult"/>.</returns>
        internal static System.Windows.Forms.DialogResult ShowWarning(string text, string caption) => System.Windows.Forms.MessageBox.Show(text, caption, System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
    }
}
