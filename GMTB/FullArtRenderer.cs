using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace GMTB
{
    class FullArtRenderer
    {
        #region Data Members
        Microsoft.Xna.Framework.Content.ContentManager Content;
        // Full Character Art
        private Texture2D mFullArtTex;
        private Vector2 mArtPos;
        #endregion
        #region Constructor
        public FullArtRenderer()
        {
            Content = Global.Content;
            mArtPos.X = 50;
            mArtPos.Y = 10;
        }
        #endregion
        #region Methods
        #endregion
    }
}
