using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Els_kom_Core.Classes
{
    public class ShowTaskbarEvent : EventArgs
    {
        public string value;

        public ShowTaskbarEvent(string showtaskbarvalue)
        {
            this.value = showtaskbarvalue;
        }
    }
}
