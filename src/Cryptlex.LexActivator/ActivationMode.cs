using System;
using System.Collections.Generic;
using System.Text;

namespace Cryptlex
{
    public class ActivationMode
    {

        public string InitialMode;

        public string CurrentMode;

        public ActivationMode(string initialMode, string currentMode)
        {
            this.InitialMode = initialMode;
            this.CurrentMode = currentMode;
        }

    }
}
