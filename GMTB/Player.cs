using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace GMTB
{
    /// <summary>
    /// Main Player class, contains all input interpretation for movement
    /// </summary>
    public class Player : AnimatingEntity, IPlayer
    {
        #region Data Members
        private PlayerIndex mPlayerNum;
        #endregion

        #region Accessors
        public new Vector2 Position
        {
            get { return mPosition; }
            set { mPosition = value; }
        }
        #endregion

        #region Constructor
        public Player()
        {
            mSpeed = 5f;
            Input.getInstance.SubscribeMove(OnNewInput);
            CollisionManager.getInstance.PlayerSubscribe(Collision, this);
            // Set movement update interval, sets the walk speed
            interval = 80f;
            Global.Player = this;
        }
        #endregion

        #region Methods
        public override void setVars(int uid, PlayerIndex pPlayer)
        {
            UID = uid;
            mPlayerNum = pPlayer;
            mUName = "Player";
            mTexturename = "Player Images/JFW";
        }
        public override void Update(GameTime gameTime)
        {
            Global.PlayerPos = mPosition;            
            // Movement controlled by timer
            timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            if (timer > interval)
            {

                mPosition += mVelocity;
                switch (mDirection)
                {
                    case "Up":
                        mTexturename = "Player Images/JBW";
                        CurrentFrame++;
                        break;
                    case "Left":
                        mTexturename = "Player Images/JLW";
                        CurrentFrame++;
                        break;
                    case "Down":
                        mTexturename = "Player Images/JFW";
                        CurrentFrame++;
                        break;
                    case "Right":
                        mTexturename = "Player Images/JRW";
                        CurrentFrame++;
                        break;
                    case "stop":
                        CurrentFrame = 0;
                        break;
                    default:
                        break;
                }
                timer = 0f;
                mVelocity.X = 0;
                mVelocity.Y = 0;
                mDirection = "stop";
                base.Update(gameTime);
            }      
        }
        public void OnNewInput(object source, InputEvent args)
        {
            if (!mVisible)
            {
                mPosition = mPrevPos;
                mVisible = true;
            }


            switch (args.currentKey)
            {
                case Keys.W:
                    mVelocity.Y = mSpeed * -1;
                    mDirection = "Up";
                    break;
                case Keys.A:
                    mVelocity.X = mSpeed * -1;
                    mDirection = "Left";
                    break;
                case Keys.S:
                    mVelocity.Y = mSpeed;
                    mDirection = "Down";
                    break;
                case Keys.D:
                    mVelocity.X = mSpeed;
                    mDirection = "Right";
                    break;
                default:
                    mVelocity.X = 0;
                    mVelocity.Y = 0;
                    break;
            }
        }

        public void CollisionHide()
        {
            mVisible = false;
        }

        public void setPos(Vector2 pos)
        {
            mPosition = pos;
        }
        #endregion

    }
}
