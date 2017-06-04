using Microsoft.Xna.Framework;

namespace GMTB.AI
{
    /// <summary>
    /// AI desigend to chase the player once then stand still
    /// </summary>
    public class JumpScare : HostileAI
    {
        public JumpScare()
        {
            mUName = "JumpScare";
            mSpeed = 0.75f;
            interval = 80f;
            mState = "Follow";
            ChaseTime = 1000f;
            mScare = true;
        }
        public override void setVars(int uid, string path)
        {
            base.setVars(uid);
            mTexturePath = path;
            mTexturename = mTexturePath + "Front";
            mDirection = "Down";
        }
        public override void Update(GameTime gameTime)
        {
            if (mScare == true)
            {
                base.Update(gameTime);
                ActivityTimer = 0f;

                timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                if (timer > interval)
                {
                    CurrentFrame++;
                    timer = 0f;
                }                            
                FrameReset();
            }
            else
            {
                mSpeed = 0f;
            }
        }

        public override void Destroy()
        {
            base.Destroy();
            mScare = false;
        }
    }
}
