using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GMTB
{
    /// <summary>
    /// Holds special animation methods for moving entites, overrides the normal static texture system
    /// </summary>
    public abstract class AnimatingEntity : Entity
    {
        #region Data Members
        // Sets frame information for the Draw Method to read the spritesheets
        protected int Frames;
        protected int CurrentFrame;
        protected int Rows;
        protected int Columns;

        // Row and Column sizes for Hitbox
        private int width;
        private int height;

        // Timers for frame transistions
        protected float timer;
        protected float interval;
        // New hitbox to override default one inherited from Entity, uses special sprite sheet details
        public override Rectangle HitBox
        {
            get { return new Rectangle((int)mPosition.X, (int)mPosition.Y, width, height); }
        }
        #endregion

        #region Constructor
        public AnimatingEntity()
        {
            // Initialise Frame information
            Frames = 4;
            CurrentFrame = 0;
            Rows = 1;
            Columns = 4;

            // Initialise timer
            timer = 0;
        }
        #endregion

        #region Methods
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            // Used to reset the animation when it reaches the end of the spritesheet
            if (CurrentFrame == Frames)
                CurrentFrame = 0;    
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            
            // Override normal draw method with specialised animating one

            // Calculate size of each animation frame
            width = mTexture.Width / Columns;
            height = mTexture.Height / Rows;
            int row = (int)((float)CurrentFrame / (float)Columns);
            int column = CurrentFrame % Columns;

            // Position selection around frame of spritesheet
            Rectangle sourceRectangle = new Rectangle(width * column, height * row, width, height);
            Rectangle destinationRectangle = new Rectangle((int)mPosition.X, (int)mPosition.Y, width, height);

            // Run spritebatch update with each frame of the animation
            spriteBatch.Draw(mTexture, destinationRectangle, sourceRectangle, Color.AntiqueWhite);            
        }
        #endregion

    }
}
