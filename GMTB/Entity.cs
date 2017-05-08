using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GMTB
{
    /// <summary>
    /// Main Entity class, everything that has a physical presence in the world inherits from this class 
    /// </summary>
    public abstract class Entity : IEntity
    {
        #region Data Members
        //--Key Variables
        protected int mUID;
        protected string mUName;
        protected bool mVisible;
        protected Vector2 mDefaultPos;
        //--Movement
        protected float mSpeed;
        protected Vector2 mPosition;
        protected bool mCollidable;
        protected string mDirection;
        protected Vector2 mVelocity;
        protected Vector2 mPrevPos;

        //--Texture
        protected Texture2D mTexture;
        protected string mTexturename;

        //--Hitbox for Collision Detection
        public virtual Rectangle HitBox
        {
            get { return new Rectangle((int)mPosition.X, (int)mPosition.Y, mTexture.Width, mTexture.Height); }
        }
        #endregion

        #region Accessors
        public int UID
        {
            get { return mUID; }
            set { mUID = value; }
        }
        public string UName
        {
            get { return mUName; }
        }
        public Texture2D aTexture
        {
            set { mTexture = value; }
        }
        public string aTexturename
        {
            get { return mTexturename; }
        }
        public Vector2 Position
        {
            get { return mPosition; }
        }
        public bool Collidable
        {
            get { return mCollidable; }
        }

        public bool Visible
        {
            get { return mVisible; }
            set { mVisible = value; }
        }

        public Vector2 DefaultPos
        {
            get { return mDefaultPos; }
            set { mDefaultPos = value; }
        }
        #endregion

        #region Constructor
        public Entity()
        {
            mCollidable = false;
            mVisible = true;          
        }
        #endregion

        #region Methods
        public virtual void setVars(int uid)
        {
            UID = uid;
        }
        public virtual void setVars(int uid, string path)
        {
            mTexturename = path;
            UID = uid;
        }
        public virtual void setVars(string path, bool Dialogue){}
        public virtual void setVars(int uid, PlayerIndex pPlayer){}
        public virtual void setVars(string tRoom, Vector2 playerStart){}

        public void setPos(int x, int y)
        {
            mPosition.X = x;
            mPosition.Y = y;
            mDefaultPos = mPosition;
        }

        public virtual void Update(GameTime gameTime)
        {
            mPrevPos = mPosition;
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(mTexture, mPosition, Color.AntiqueWhite);
        }
        public virtual void Collision(object source, CollisionEvent args)
        {
            mVelocity.X = 0;
            mVelocity.Y = 0;
            mPosition = mPrevPos;
        }
        public virtual void Destroy()
        {
            mPosition.X = 1000;
            mPosition.Y = 1000;
        }

        public virtual void sub()
        {

        }

        
        #endregion
    }
}
