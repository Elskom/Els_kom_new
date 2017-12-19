namespace Els_kom_Core.Classes
{
    /// <summary>
    /// Event Fired when The form is shown in taskbar.
    /// </summary>
    public class ShowTaskbarEvent : System.EventArgs
    {
        /// <summary>
        /// The value of the string the class was initialized with. This value is read only.
        /// </summary>
        public readonly string value;

        /// <summary>
        /// Sets the value to pass to the Event that is being fired.
        /// </summary>
        /// <param name="showtaskbarvalue"></param>
        public ShowTaskbarEvent(string showtaskbarvalue)
        {
            this.value = showtaskbarvalue;
        }
    }
}
