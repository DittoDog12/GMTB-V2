using Microsoft.Xna.Framework;

namespace GMTB.AI
{
    /// <summary>
    /// AI desigend to chase the player once then stand still
    /// </summary>
    class JumpScare : HostileAI
    {
        public JumpScare()
        {
            mUName = "JumpScare";
            mSpeed = 0.75f;
            interval = 75f;
            mState = "Follow";

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
                mDistanceToDest = mPlayerPos - mPosition;
                mDistanceToDest.Normalize();

                mVelocity = mDistanceToDest * mSpeed;
                CurrentFrame++;
                base.Update(gameTime);
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
