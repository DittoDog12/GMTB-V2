using Microsoft.Xna.Framework;

namespace GMTB.AI
{
    /// <summary>
    /// low Difficulty AI, inherits basic Behaviour from Hostile AI but alters timings
    /// </summary>   
    class LowLevelAI : HostileAI
    {
        #region Constructor
        public LowLevelAI()
        {
            mProximityBoxSize = new Vector2(150, 150);

            ChaseTime = 1000f;
            SearchTime = 1000f;
            mCollidable = true;
            interval = 100f;
            mSpeed = 2f;
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
        #endregion
    }
}
