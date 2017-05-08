using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GMTB
{
    /// <summary>
    /// Game Over screen
    /// </summary>
    public class GameOver
    {
        #region Data Members
        Microsoft.Xna.Framework.Content.ContentManager Content;
        private Texture2D exitButton;
        private Vector2 exitPosition;

        //private Texture2D startButton;
        //private Vector2 startPosition;

        MouseState mouseState;

        MouseState previousMouseState;
        #endregion

        #region Constructor
        public GameOver()
        {
            Content = Global.Content;
        }
        #endregion

        #region Methods
        public void Initialize(SpriteBatch spriteBatch)
        {
            RoomManager.getInstance.Room = "Backgrounds/GameOver";
            // create Exit button, positioned center, near the bottom
            exitPosition = new Vector2(Kernel.ScreenWidth - (Kernel.ScreenWidth / 4), Kernel.ScreenHeight - 75);
            exitButton = Content.Load<Texture2D>("Exit");

            // create Start button, position a quarter the distance across the screen from the left, near the bottom
            //startPosition = new Vector2(Kernel.ScreenWidth / 4, Kernel.ScreenHeight - 75);
            //startButton = Content.Load<Texture2D>("start");
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(exitButton, exitPosition, Color.White);
        }
        public void Update(GameTime gameTime)
        {
            mouseState = Mouse.GetState();

            if (previousMouseState.LeftButton == ButtonState.Pressed && mouseState.LeftButton == ButtonState.Released)
                MouseClicked(mouseState.X, mouseState.Y);

            previousMouseState = mouseState;
        }
        public void MouseClicked(int x, int y)
        {
            // Create a Rectangle around the mouse click position
            Rectangle mouseClickedRect = new Rectangle(x, y, 10, 10);

            Rectangle exitRect = new Rectangle((int)exitPosition.X, (int)exitPosition.Y, exitButton.Width, exitButton.Height);
            //Rectangle startRect = new Rectangle((int)startPosition.X, (int)startPosition.Y, startButton.Width, startButton.Height);

            if (mouseClickedRect.Intersects(exitRect))
                Kernel._gameState = Kernel.GameStates.Exiting;
            //if (mouseClickedRect.Intersects(startRect))
            //    Kernel.menu1.LoadGame();
        }
        #endregion
    }
}
