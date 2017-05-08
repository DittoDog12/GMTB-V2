using Microsoft.Xna.Framework;

namespace GMTB
{
    /// <summary>
    /// Collectable item
    /// </summary>
    class Item : Entity, Collidable, hasProximity, IItem
    {
        #region Data Members
        private bool mCollected = false;
        private bool use = false;

        public virtual Rectangle ProximityBox
        {
            get
            {
                return new Rectangle((int)mPosition.X - mTexture.Width / 2, (int)mPosition.Y,
              mTexture.Width * 2, mTexture.Height * 2);
            }
        }
        #endregion
        #region Accessors
        public bool Collected
        {
            get { return mCollected; }
        }
        #endregion
        #region Constructor
        public Item()
        {
            sub();
        }
        #endregion

        #region Methods
        public void inProximity(object source, ProximityEvent args)
        {
            if (args.Entity == this)
            {
                if (use == true)
                {
                    mCollected = true;
                    Destroy();
                    use = false;
                }
            }

        }

        public void OnUse(object source, InputEvent args)
        {
            use = true;
        }
        public override void Destroy()
        {
            base.Destroy();
            Input.getInstance.UnSubscribeUse(OnUse);
            CollisionManager.getInstance.unSubscribe(Collision, this);
            ProximityManager.getInstance.unSubscribe(inProximity, this);
            mVisible = false;
        }
        public override void sub()
        {
            if (mCollected == false)
            {
                mVisible = true;
                Input.getInstance.SubscribeUse(OnUse);
                CollisionManager.getInstance.Subscribe(Collision, this);
                ProximityManager.getInstance.Subscribe(inProximity, this);
            }  
        }
        #endregion
    }
}
