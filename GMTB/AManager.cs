using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace GMTB
{
    /// <summary>
    /// Can be used for any extra managers in the game to reveal themselves to the core manager for the update loop
    /// </summary>
    public abstract class AManager
    {
        public abstract void Update(GameTime gameTime);
    }
}