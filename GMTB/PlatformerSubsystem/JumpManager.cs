using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GMTB;
using Microsoft.Xna.Framework;

namespace GMTB.PlatformerSubsys
{
    #region JumpEvent
    /// <summary>
    /// Jump event mini public class
    /// </summary>
    /// 
    public class JumpEvent : EventArgs
    {
        public hasJump Mario;
        public JumpEvent(hasJump mario)
        {
            Mario = mario;
        }
    }
    #endregion

    public class JumpManager : AManager
    {
        #region Data Members
        private static JumpManager Instance = null;
        private hasJump mPlayer;
        private List<ISolidObject> AllGround;
        private SolidGround LastGround;

        public event EventHandler<JumpEvent> Jump;
        public event EventHandler<JumpEvent> canTJump;
        #endregion

        #region Constructor
        private JumpManager()
        {
            AllGround = new List<ISolidObject>();
            CoreManager.getInstance.AddAdditionalManagers(this);
        }
        public static JumpManager getInstance
        {
            get
            {
                if (Instance == null)
                    Instance = new JumpManager();
                return Instance;
            }
        }
        #endregion

        #region Methods
        public void addGround(ISolidObject entity)
        {
            AllGround.Add(entity);
        }
        public void removeGround(ISolidObject entity)
        {
            AllGround.Remove(entity);
        }
        public void Subscribe(EventHandler<JumpEvent> handler, hasJump entity)
        {
            Jump += handler;
            mPlayer = entity;
        }
        public void unSubscribe(EventHandler<JumpEvent> handler, hasJump entity)
        {
            Jump -= handler;
            mPlayer = null;
        }
        public void SubscribeCant(EventHandler<JumpEvent> handler, hasJump entity)
        {
            canTJump += handler;
            mPlayer = entity;
        }
        public void unSubscribeCant(EventHandler<JumpEvent> handler, hasJump entity)
        {
            canTJump -= handler;
            mPlayer = null;
        }
        public virtual void canJump(SolidGround g)
        {
            LastGround = g;
            JumpEvent args = new JumpEvent(mPlayer);
            Jump(this, args);
        }
        public virtual void cantJump()
        {
            JumpEvent args = new JumpEvent(mPlayer);
            canTJump(this, args);
        }
        public override void Update(GameTime gameTime)
        {
            foreach (SolidGround g in AllGround)
                if (mPlayer.HitBox.Intersects(g.ProximityBox))
                    canJump(g);
            if (LastGround != null)
                if (!mPlayer.HitBox.Intersects(LastGround.ProximityBox))
                    cantJump();
        }
        #endregion
    }
}
