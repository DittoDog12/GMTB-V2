using Microsoft.Xna.Framework;

namespace GMTB
{
    /// <summary>
    /// Wall to stop player or AI from leaving the world
    /// </summary>
    class InvisibleWall : IWall
    {
        #region Data Members
        private Vector2 mPosition;
        private Vector2 mSoize;
        #endregion
        #region Accessors
        public virtual Rectangle HitBox
        {
            get { return new Rectangle((int)mPosition.X, (int)mPosition.Y, (int)mSoize.X, (int)mSoize.Y); }
        }
        #endregion
        #region Constructor
        public InvisibleWall()
        {
            Sub();
        }
        #endregion
        #region Methods
        public void setVars(Vector2 pos, Vector2 size)
        {
            mPosition = pos;
            mSoize = size;
        }
        public void Sub()
        {
            CollisionManager.getInstance.Walls.Add(this);
        }
        public void UnSub()
        {
            CollisionManager.getInstance.Walls.Remove(this);
        }
        #endregion
    }
}
