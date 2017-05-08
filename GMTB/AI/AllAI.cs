using Microsoft.Xna.Framework;

namespace GMTB.AI
{
    /// <summary>
    /// Holds shared behaviour and varibles for all AI
    /// </summary>
    public class AllAI : AnimatingEntity, IAI
    {
        #region Data Members
        protected Vector2 mPlayerPos;
        protected bool mPlayerVisible;
        protected string mState;
        protected string mTexturePath;
        protected bool mScare;
        protected int mWalkDir = -1;
        protected bool mPatrolVert;
        #endregion

        #region Accessors
        public Vector2 PlayerPos
        {
            set { mPlayerPos = value; }
        }
        public bool PlayerVisible
        {
            set { mPlayerVisible = value; }
        }
        public string State
        {
            get { return mState; }
        }
        public bool PatrolVert
        {
            set { mPatrolVert = value; }
        }
        #endregion

        #region Constructor
        public AllAI()
        {
            mPlayerVisible = true;
        }
        #endregion

        #region Methods
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            mPosition += mVelocity;
            // State controller
            switch (mState)
            {
                case "Idle":
                    Idle();
                    break;
                case "Stand":
                    Stand();
                    break;
                default:
                    break;
            }
        }

        public bool CollisionChecker()
        {
            bool rtnVal = false;

            return rtnVal;
        }

        public override void Collision(object source, CollisionEvent args)
        {
            if (args.Entity == this && args.Wall != null)
            {
                mWalkDir *= -1;

                if (mPatrolVert == true)
                    mPosition.Y = mPrevPos.Y + (10 * mWalkDir);
                else
                    mPosition.X = mPrevPos.X + (10 * mWalkDir);
            }
        }

        public virtual void Idle()
        {
            if (mPatrolVert == true)
                mVelocity.Y = mSpeed * mWalkDir;
            else
                mVelocity.X = mSpeed * mWalkDir;
        }
        public virtual void Stand()
        {
            mVelocity.X = 0;
            mVelocity.Y = 0;
        }

        public virtual void setVars(bool PatrolVert, string state) { }
        public virtual void setVars(bool PatrolVert, string state, string dir) { }
        #endregion
    }
}
