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

        public bool isSubbed = false;

        private Texture2D exitButton;
        private Vector2 exitPosition;

        private Texture2D startButton;
        private Vector2 startPosition;

        private Texture2D AButton;
        private Vector2 APosition;

        private Texture2D BButton;
        private Vector2 BPosition;

        MouseState mouseState;

        MouseState previousMouseState;
        #endregion

        #region Constructor
        public GameOver()
        {
            Content = Global.Content;

            // create Exit button, positioned center, near the bottom
            exitPosition = new Vector2(Global.ScreenWidth - (Global.ScreenWidth / 4), Global.ScreenHeight - 75);
            exitButton = Content.Load<Texture2D>("Exit");

            // create Start button, position a quarter the distance across the screen from the left, near the bottom
            startPosition = new Vector2(Global.ScreenWidth / 4, Global.ScreenHeight - 75);
            startButton = Content.Load<Texture2D>("start");

            // Create A and B Buttons will only render if controller connected, allows for on the fly adjustment

            APosition = new Vector2((Global.ScreenWidth / 4) + 30, Global.ScreenHeight - 100);
            AButton = Content.Load<Texture2D>("A-Button");

            BPosition = new Vector2(Global.ScreenWidth - (Global.ScreenWidth / 4) + 30, Global.ScreenHeight - 100);
            BButton = Content.Load<Texture2D>("B-Button");
        }
        #endregion

        #region Methods
        public void Initialize(SpriteBatch spriteBatch)
        {
            Sub();
            RoomManager.getInstance.Room = "Backgrounds/GameOver";
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(exitButton, exitPosition, Color.White);
            spriteBatch.Draw(startButton, startPosition, Color.White);
            if (Input.getInstance.CheckController())
            {
                spriteBatch.Draw(AButton, APosition, Color.White);
                spriteBatch.Draw(BButton, BPosition, Color.White);
            }
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
            Rectangle startRect = new Rectangle((int)startPosition.X, (int)startPosition.Y, startButton.Width, startButton.Height);

            if (mouseClickedRect.Intersects(exitRect))
                Exit();
            if (mouseClickedRect.Intersects(startRect))
                restart();
                
        }

        public void Exit()
        {
            Global.GameState = Global.availGameStates.Exiting;
        }

        public void restart()
        {
            // Clear Scene
            SceneManager.getInstance.Entities.Clear();
            SceneManager.getInstance.SceneGraph.Clear();

            // Reset Levels
            LevelManager.getInstance.NewGameReset();

            // Clear AI's
            AiManager.getInstance.AIs.Clear();

            // Clear Event Triggers
            ProximityManager.getInstance.All.Clear();
            CollisionManager.getInstance.All.Clear();

            // Clear All Entities
            EntityManager.getInstance.Entities.Clear();

            // Initiate loading of new game
            Global.GameState = Global.availGameStates.Loading;
            MenuManager.getInstance.MainMenu().Start();
        }

        public void Sub()
        {
            Input.getInstance.SubscribeGPMenu(GP);
            isSubbed = true;
        }
        public void unSub()
        {
            Input.getInstance.UnSubscribeGPMenu(GP);
            isSubbed = false;
        }

        public void GP(object source, GPEvent args)
        {
            if (args.currentState == "A")
                restart();
            if (args.currentState == "B")
                Exit();
        }
        #endregion
    }
}
