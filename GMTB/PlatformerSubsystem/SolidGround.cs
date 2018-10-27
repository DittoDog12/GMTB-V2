using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using GMTB;
using Microsoft.Xna.Framework.Graphics;

namespace GMTB.PlatformerSubsys
{
    public class SolidGround : SolidObject, ISolidObject
    {
        #region Data Members
        // Second Hitbox for proximity detection
        public virtual Rectangle ProximityBox
        {
            get
            {
                return new Rectangle((int)mPosition.X, (int)mPosition.Y - 5,
              mTexture.Width, 5);
            }
        }
        #endregion

        #region Constructor
        public SolidGround()
        {
            JumpManager.getInstance.addGround(this);
        }
        #endregion
    }
}
