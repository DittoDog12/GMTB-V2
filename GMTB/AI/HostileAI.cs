using Microsoft.Xna.Framework;

namespace GMTB.AI
{
 /// <summary>
 /// Main hostile AI behaviours, difficulty modified by child classes
 /// </summary>
 public class HostileAI : AllAI, Collidable, hasProximity
    {
        #region Data Members
        protected Vector2 mStartPos;
        protected Vector2 mDistanceToDest;
        protected float ChaseTime;
        protected float SearchTime;
        protected float ActivityTimer;
        protected float SearchTimer;
        private bool beginFollow;

        // Second Hitbox for proximity detection
        // Box Size set by Specific AI
        protected Vector2 mProximityBoxSize;
        public virtual Rectangle ProximityBox
        {
            get
            {
                return new Rectangle((int)mPosition.X - (int)mProximityBoxSize.X / 2, (int)mPosition.Y - (int)mProximityBoxSize.Y / 2,
              (int)mProximityBoxSize.X, (int)mProximityBoxSize.Y);
            }
        }
        #endregion

        #region Constructor
        public HostileAI()
        {
            mState = "Idle";
            //mVelocity.Y = mSpeed;
            mStartPos = mPosition;
            mUName = "AI";
            sub();
        }
        #endregion

        #region Methods
        public override void setVars(bool PatrolVert, string state)
        {
            mState = state;
            mPatrolVert = PatrolVert;
        }
        public override void setVars(bool PatrolVert, string state, string dir)
        {
            setVars(PatrolVert, state);
            mDirection = dir;
        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            checkPlayerProx(gameTime);
            
            if (mVelocity.X > 0)
                mDirection = "Right";
            else if (mVelocity.X < 0)
                mDirection = "Left";

            if (mVelocity.X == 0)
            {
                if (mVelocity.Y > 0)
                    mDirection = "Down";
                else if (mVelocity.Y < 0)
                    mDirection = "Up";
            }

            switch (mState)
            {
                case "Follow":
                    mWalkDir = 1;
                    mSpeed *= mWalkDir;
                    FollowPlayer(gameTime);
                    break;
                case "Search":
                    Search(gameTime);
                    break;
                case "Reset":
                    Reset();
                    break;
                default:
                    break;
            }

        }
        public void FollowPlayer(GameTime gameTime)
        {
            ActivityTimer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            mDistanceToDest = mPlayerPos - mPosition;
            mDistanceToDest.Normalize();
            mVelocity = mDistanceToDest * mSpeed;

            if (ActivityTimer >= ChaseTime || !mPlayerVisible)
            {
                mState = "Search";
                ActivityTimer = (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                SearchTimer = 0f;
                mDirection = "Left";
                mVelocity.Y = 0;
                mVelocity.X = 0;
            }
        }
        public void Search(GameTime gameTime)
        {
            SearchTimer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            ActivityTimer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            if (SearchTimer >= (SearchTime / 4 - 10) && SearchTimer <= (SearchTime / 4 + 10))
                mDirection = "Up";
            if (SearchTimer >= (SearchTime / 2 - 10) && SearchTimer <= (SearchTime / 2 + 10))
                mDirection = "Right";
            if (SearchTimer >= (SearchTime - ((SearchTime / 4) - 10)) &&
                SearchTimer <= (SearchTime - ((SearchTime / 4) + 10)))
                mDirection = "Down";

            if (ActivityTimer >= SearchTime)
            {

                mState = "Reset";
            }
        }
        public override void Collision(object source, CollisionEvent args)
        {
            base.Collision(source, args);
            if (args.Entity == this && args.Wall == null)
            {
                RoomManager.getInstance.Room = "Backgrounds/GameOver";
                Kernel._gameState = Kernel.GameStates.GameOver;
            }
        }
        public void inProximity(object source, ProximityEvent args)
        {
            if (args.Entity == this)
            {
                //if (mState == "Idle")
                    beginFollow = true;
            }

        }
        public void checkPlayerProx(GameTime gameTime)
        {
            if (beginFollow == true)
            {
                mState = "Follow";
                ActivityTimer = (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                beginFollow = false;
            }
            //if (mState == "Idle")
            //{
            //    if (mPlayerPos.X >= Kernel.ScreenWidth / 2)
            //    {
            //        mState = "Follow";
            //        ActivityTimer = (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            //    }
            //}
        }
        public void Reset()
        {
            Vector2 start = mStartPos;
            start.Normalize();
            mDistanceToDest = start - mPosition;
            mDistanceToDest.Normalize();
            mVelocity = mDistanceToDest * mSpeed;
            if ((mPosition.X >= mStartPos.X -5 && mPosition.X <= mStartPos.X + 5) 
                && (mPosition.Y >= mStartPos.Y - 5 && mPosition.Y <= mStartPos.Y + 5))
            {
                mState = "Idle";
                mVelocity.X = 0;
                mVelocity.Y = mSpeed;
                Destroy();
            }
        }

        public override void Destroy()
        {
            base.Destroy();
            mState = "Stop";
            CollisionManager.getInstance.unSubscribe(Collision, this);
            ProximityManager.getInstance.unSubscribe(inProximity, this);
        }
        public override void sub()
        {
            CollisionManager.getInstance.Subscribe(Collision, this);
            ProximityManager.getInstance.Subscribe(inProximity, this);
        }
        #endregion
    }
}
