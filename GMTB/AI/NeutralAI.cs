using Microsoft.Xna.Framework;

namespace GMTB.AI
{
    /// <summary>
    /// Neutral AI, simply follows player, inherits directly from Entity as it doesnt need to animate
    /// </summary>
    class NeutralAI : Entity, Collidable, INeutralAI
    {
        #region Data Members
        private string mTexturePath;
        private Vector2 mPlayerPos;
        private int TimesHit = 0;
        #endregion
        #region Acessors
        public Vector2 PlayerPos
        {
            set { mPlayerPos = value; }
        }
        #endregion
        #region Constructor
        public NeutralAI() : base()
        {
            CollisionManager.getInstance.Subscribe(Collision, this);
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
            FollowPlayer();
            base.Update(gameTime);
            switch (mDirection)
            {
                case "Left":
                    mTexturename = mTexturePath + "Left";
                    break;
                case "Down":
                    mTexturename = mTexturePath + "Front";
                    break;
                case "Right":
                    mTexturename = mTexturePath + "Right";
                    break;

                default:
                    break;
            }
            mDirection = "stop";
        }
        public void FollowPlayer()
        {
            if (mPlayerPos.X > mPosition.X)
                mDirection = "Right";
            if (mPlayerPos.X < mPosition.X)
                mDirection = "Left";
            if (mPlayerPos.X == mPosition.X)
                mDirection = "Down";
            //if (mPlayerPos.Y > mPosition.Y)
            //    mDirection = "Right";
            //if (mPlayerPos.Y < mPosition.Y)
            //    mDirection = "Left";
            //if (mPlayerPos.Y == mPosition.Y)
            //    mDirection = "Down";

        }

        public override void Collision(object source, CollisionEvent args)
        {
            if (args.Entity == this && args.Wall == null)
            {

                base.Collision(source, args);
                if (TimesHit == 4)
                {
                    Script.getInstance.SingleDialogue("Stop That");
                    TimesHit++;
                }
                else if (TimesHit == 7)
                {
                    Script.getInstance.SingleDialogue("STOP IT!");
                    TimesHit++;
                }
                else if (TimesHit == 10)
                {
                    Script.getInstance.SingleDialogue("STOP!!!");
                    TimesHit++;
                }
                else if (TimesHit == 12)
                {
                    Script.getInstance.SingleDialogue("What are you even doing?");
                    TimesHit++;
                }
                else if (TimesHit == 15)
                {
                    Script.getInstance.SingleDialogue("I don't even");
                    TimesHit++;
                }
                else if (TimesHit == 17)
                {
                    Script.getInstance.SingleDialogue("*Sigh* 'Give me the Booty', there I said it");
                    TimesHit++;
                }
                else if (TimesHit >= 20)
                {
                    Script.getInstance.SingleDialogue("I said it once I won't say it again");
                    TimesHit++;
                }
                else
                    TimesHit++;
            }
        }
        #endregion
    }
}
