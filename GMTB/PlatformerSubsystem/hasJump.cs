using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace GMTB.PlatformerSubsys
{
    public interface hasJump
    {
        Rectangle HitBox { get; }
        void Jump(object source, JumpEvent args);
    }
}
