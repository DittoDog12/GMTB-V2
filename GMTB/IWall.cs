using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace GMTB
{
    /// <summary>
    /// Wall specific interface
    /// </summary>
    public interface IWall
    {
        Rectangle HitBox { get; }
        void setVars(Vector2 pos, Vector2 size);
        void Sub();
        void UnSub();
    }
}
