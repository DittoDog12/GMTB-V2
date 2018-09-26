using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

// Using http://www.paradeofrain.com/tutorials/xna-camera-2d/
// Using https://stackoverflow.com/questions/712296/xna-2d-camera-engine-that-follows-sprite

namespace GMTB
{

    public class Camera2D
    {
        #region Data Members
        private Vector2 mPosition;
        protected float mZoom;
        protected float mRotatation;
        public Matrix mTransform;
        public Vector2 mVelocity;
        public float mSpeed = 2.5f;

        private int ScreenHeight;
        private int ScreenWidth;

        // Follow Player Varaibles, Based on AI system
        //private Vector2 mDistanceToDest;
        private Vector2 mPlayerPos;

        // Part of Broken Ideas
        //private float mSpeedModifier = 7.5f;
        //private bool mMove = true;
        #endregion

        #region Accessors
        public Vector2 Position
        {
            get { return mPosition; }
            set { mPosition = value; }
        }
        public float Zoom
        {
            get { return mZoom; }
            set
            {
                mZoom = value; if (mZoom < 0.1f)
                    mZoom = 0.1f;
            }
        }
        public float Rotation
        {
            get { return mRotatation; }
            set { mRotatation = value; }
        }

        #endregion

        #region Constructor
        public Camera2D()
        {
            ScreenHeight = Global.ScreenHeight;
            ScreenWidth = Global.ScreenWidth;
            mZoom = 0.75f;
            mRotatation = 0.0f;
            mPosition = Vector2.Zero;
        }
        #endregion

        #region Methods
        //public void Move(Vector2 vector)
        //{
        //    if (vector.X > 0)
        //        vector.X -= mSpeedModifier;
        //    if (vector.X < 0)
        //        vector.X += mSpeedModifier;
        //    if (mMove)
        //        mPosition += vector;
        //    mMove = true;
        //}
        //public void MoveControl(bool move)
        //{
        //    mMove = move;
        //}
        public void UpdateX(float x)
        {
            mPosition.X = x;
        }
        public void UpdateY(float y)
        {
            mPosition.Y = y;
        }

        public Matrix GetTransform(GraphicsDevice graphicsDevice)
        {
            mTransform = Matrix.CreateTranslation(new Vector3(-mPosition.X, -mPosition.Y, 0)) *
                Matrix.CreateRotationZ(Rotation) *
                Matrix.CreateScale(new Vector3(mZoom, mZoom, 1)) *
                Matrix.CreateTranslation(new Vector3(graphicsDevice.Viewport.Width * 0.5f, graphicsDevice.Viewport.Width * 0.5f, 0));

            return mTransform;
        }

        public void Update(GameTime gameTime)
        {
            mPlayerPos = Global.PlayerPos;
            var delta = (float)gameTime.ElapsedGameTime.TotalSeconds;

            mPosition.X += (mPlayerPos.X - mPosition.X) * mSpeed * delta;
            mPosition.Y += (mPlayerPos.Y - mPosition.Y) * mSpeed * delta;

            // Based on AI tracking system - Kinda Janky also glitches out
            //mDistanceToDest = mPlayerPos - mPosition;
            //mDistanceToDest.Normalize();
            //mVelocity = mDistanceToDest * mSpeed;
            //if (mVelocity.X == null)
            //    mVelocity.X = 0;
            //if (mVelocity.Y == null)
            //    mVelocity.Y = 0;

            //mPosition += mVelocity;
        }
        #endregion
    }
}
