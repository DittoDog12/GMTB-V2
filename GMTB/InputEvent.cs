using System;
using Microsoft.Xna.Framework.Input;

namespace GMTB
{
    /// <summary>
    /// Input event trigger
    /// </summary>
    public class InputEvent : EventArgs
    {
        public Keys currentKey;

        public InputEvent(Keys key)
        {
            currentKey = key;
        }
    }
}
