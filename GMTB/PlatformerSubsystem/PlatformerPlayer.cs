using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GMTB;
using Microsoft.Xna.Framework;

namespace GMTB.PlatformerSubsys
{
    public class PlatformerPlayer : Player, hasJump
    {
        #region Data Members
        // Jumping
        private bool mJumpable = false;
        private bool mJumping = false;
        private float mJumpSpeed = 8f;
        private float mJumpHeight = 100f;
        private float mJumpTo;

        // Gravity
        private float mGravity = 15f;

        // Camera Manipulation
        private bool camMove = true;
        #endregion

        #region Constructor
        public PlatformerPlayer() : base()
        {
            // Animation interval
            interval = 60f;
            Input.getInstance.SubscribeMove(OnNewInput);
            Input.getInstance.Unsubscribe(base.OnNewInput);
            Input.getInstance.SubscribeSpace(OnNewInput);
            JumpManager.getInstance.Subscribe(Jump, this);
            JumpManager.getInstance.SubscribeCant(CantJump, this);
        }
        #endregion

        #region Accessors
        public float XVelocity
        {
            get { return mVelocity.X; }
        }
        public bool CamMove
        {
            get { return camMove; }
        }
        #endregion

        #region Methods
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (mJumping == true)
            {
                if (mPosition.Y > mJumpTo)
                    mVelocity.Y = mJumpSpeed * -1;
                else if (mPosition.Y < mJumpTo)
                {
                    mJumping = false;
                    mJumpable = false;
                }                  
            }
            else
                Applygravity();
        }
        public void Applygravity()
        {
            if (!mJumpable)
                mVelocity.Y = mGravity;
        }
        public void BeginJump()
        {
            mJumpTo = mPosition.Y - mJumpHeight;
            mJumping = true;
            mJumpable = false;
        }
        public void Jump(object source, JumpEvent args)
        {
            if (args.Mario == this)
                mJumpable = true;
        }
        public void CantJump(object source, JumpEvent args)
        {
            if (args.Mario == this)
                mJumpable = false;
        }
        public override void OnNewInput(object source, InputEvent args)
        {
            switch (args.currentKey)
            {
                case Microsoft.Xna.Framework.Input.Keys.W:
                    break;
                case Microsoft.Xna.Framework.Input.Keys.S:
                    break;
                case Microsoft.Xna.Framework.Input.Keys.Space:
                    if (mJumpable)
                        BeginJump();
                    break;
                default:
                    base.OnNewInput(source, args);
                    break;
            }
        }
        #endregion
    }
}
