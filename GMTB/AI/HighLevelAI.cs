using Microsoft.Xna.Framework;

namespace GMTB.AI
{
    /// <summary>
    /// High Difficulty AI, inherits basic Behaviour from Hostile AI but alters timings
    /// </summary>
    public class HighLevelAI : HostileAI
    {
        #region Data Members
        private float WaitTimer;
        private bool runonce = false;
        private float waiting;
        #endregion

        #region Constructor
        public HighLevelAI() : base()
        {
            mProximityBoxSize = new Vector2(250, 250);
            ChaseTime = 2000f;
            SearchTime = 2000f;
            mCollidable = true;
            mSpeed = 3f;
            interval = 100f;
            WaitTimer = 2000f;
           // Reset();
        }
        #endregion

        #region Methods
        public override void setVars(int uid, string path)
        {
            base.setVars(uid);
            mTexturePath = path;
            mTexturename = mTexturePath + "Front";
        }

        public override void Update(GameTime gameTime)
        {   
            switch (mState)
            {
                case "Wait":
                    Wait(gameTime);
                    break;
                default:
                    break;
            } 
            timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            if (timer > interval)
            {              
                switch (mDirection)
                {
                    case "Up":
                        mTexturename = mTexturePath + "Back";
                        CurrentFrame++;
                        break;
                    case "Left":
                        mTexturename = mTexturePath + "Left";
                        CurrentFrame++;
                        break;
                    case "Down":
                        mTexturename = mTexturePath + "Front";
                        CurrentFrame++;
                        break;
                    case "Right":
                        mTexturename = mTexturePath + "Right";
                        CurrentFrame++;
                        break;
                    case "stop":
                        CurrentFrame = 0;
                        break;
                    default:
                        break;
                }
                timer = 0f;
                mDirection = "stop";
                base.Update(gameTime);
            }
           
        }
        public void Wait(GameTime gameTime)
        {
            if (runonce == false)
            {
                CollisionManager.getInstance.unSubscribe(Collision, this);
                ProximityManager.getInstance.unSubscribe(inProximity, this);
                waiting = 0f;
                runonce = true;
            }
            Visible = false;
            if (waiting >= WaitTimer)
            {
                Visible = true;   
                CollisionManager.getInstance.Subscribe(Collision, this);
                ActivityTimer = (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                SearchTimer = 0f;
                mDirection = "Left";
                mVelocity.Y = 0;
                mVelocity.X = 0;
                mState = "Search";
            }
            waiting += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
        }
        #endregion
    }
}
