namespace Els_kom_Core.Classes
{
    /// <summary>
    /// Els_kom's Generic MessageBox Manager.
    /// </summary>
    public static class MessageManager
    {
        /// <summary>
        /// Shows an MessageBox that is for an Error.
        /// </summary>
        public static System.Windows.Forms.DialogResult ShowError(string text, string caption) {
            return System.Windows.Forms.MessageBox.Show(text, caption, System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
        }

        /// <summary>
        /// Shows an MessageBox that is for information.
        /// </summary>
        public static System.Windows.Forms.DialogResult ShowInfo(string text, string caption) {
            return System.Windows.Forms.MessageBox.Show(text, caption, System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
        }

        /// <summary>
        /// Shows an MessageBox that is for an Warning.
        /// </summary>
        public static System.Windows.Forms.DialogResult ShowWarning(string text, string caption) {
            return System.Windows.Forms.MessageBox.Show(text, caption, System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
        }
    }
}
