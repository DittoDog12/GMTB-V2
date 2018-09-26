using GMTB;
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GMTB
{
    /// <summary>
    /// Main menu, displayed on start up and triggers first level load
    /// </summary>
    public class MainMenu
    {
        #region Data Members
        Microsoft.Xna.Framework.Content.ContentManager Content;

        public bool isSubbed = false;

        private Texture2D startButton;
        private Vector2 startPosition;

        private Texture2D exitButton;
        private Vector2 exitPosition;

        private Texture2D loadButton;
        private Vector2 loadPosition;

        MouseState mouseState;

        MouseState previousMouseState;

        private IEntity createdEntity;

        private Texture2D AButton;
        private Vector2 APosition;

        private Texture2D BButton;
        private Vector2 BPosition;

        private Texture2D XButton;
        private Vector2 XPosition;
        #endregion

        #region Constructor
        public MainMenu()
        {
            Content = Global.Content;
            RoomManager.getInstance.Room = "Backgrounds/MainMenu";
        }
        #endregion

        #region Methods
        public void Initialize(SpriteBatch spriteBatch)
        {
            Sub();
            // create Start button, position a quarter the distance across the screen from the left, near the bottom
            startPosition = new Vector2(Global.ScreenWidth /4, Global.ScreenHeight - 75);
            startButton = Content.Load<Texture2D>("start");

            // create Exit button, position a quarter the distance across the screen from the right, near the bottom
            exitPosition = new Vector2(Global.ScreenWidth - (Global.ScreenWidth / 4), Global.ScreenHeight - 75);
            exitButton = Content.Load<Texture2D>("Exit");

            // create load button, position it center, offset by texture width
            loadButton = Content.Load<Texture2D>("load");
            loadPosition = new Vector2(Global.ScreenWidth - ((Global.ScreenWidth / 2) - (loadButton.Width / 2)), Global.ScreenHeight - 50);

            // Create A and B Buttons will only render if controller connected, allows for on the fly adjustment

            APosition = new Vector2((Global.ScreenWidth / 4) + 30, Global.ScreenHeight - 100);
            AButton = Content.Load<Texture2D>("A-Button");

            BPosition = new Vector2(Global.ScreenWidth - (Global.ScreenWidth / 4) + 30, Global.ScreenHeight - 100);
            BButton = Content.Load<Texture2D>("B-Button");

            XPosition = new Vector2(Global.ScreenWidth - ((Global.ScreenWidth / 2) - (loadButton.Width / 2)) + 30, Global.ScreenHeight - 100);
            XButton = Content.Load<Texture2D>("X-Button");
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            RoomManager.getInstance.Draw(spriteBatch);
            spriteBatch.Draw(startButton, startPosition, Color.White);
            spriteBatch.Draw(exitButton, exitPosition, Color.White);
            spriteBatch.Draw(loadButton, loadPosition, Color.White);
            if (Input.getInstance.CheckController())
            {
                spriteBatch.Draw(AButton, APosition, Color.White);
                spriteBatch.Draw(BButton, BPosition, Color.White);
                spriteBatch.Draw(XButton, XPosition, Color.White);
            }
            spriteBatch.End();
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

            Rectangle startRect = new Rectangle((int)startPosition.X, (int)startPosition.Y, startButton.Width, startButton.Height);
            Rectangle exitRect = new Rectangle((int)exitPosition.X, (int)exitPosition.Y, exitButton.Width, exitButton.Height);
            Rectangle loadRect = new Rectangle((int)loadPosition.X, (int)loadPosition.Y, loadButton.Width, loadButton.Height);

            if (mouseClickedRect.Intersects(startRect))
            {
                Start();
                // Depreciated, kernel now creates Player
                //LoadGame();

            }
            else if (mouseClickedRect.Intersects(exitRect))
                Exit();
            else if (mouseClickedRect.Intersects(loadRect))
                Load();
        }
        public void Sub()
        {
            Input.getInstance.SubscribeExit(onEsc);
            Input.getInstance.SubscribeGPMenu(GP);
            isSubbed = true;
        }
        public void unSub()
        {
            Input.getInstance.unSubscribeExit(onEsc);
            Input.getInstance.UnSubscribeGPMenu(GP);
            isSubbed = false;
        }
        public void onEsc(object source, EventArgs args)
        {
            Exit();
        }
        public void GP(object source, GPEvent args)
        {
            if (args.currentState == "A")
                Start();
            if (args.currentState == "B")
                Exit();
            if (args.currentState == "X")
                Load();
        }
        public void Start()
        {
            unSub();
            Global.GameState = Global.availGameStates.Loading;
        }
        public void Exit()
        {
            unSub();
            Global.GameState = Global.availGameStates.Exiting;
        }
        public void Load()
        {
            unSub();
            SceneManager.getInstance.InitiateLoad();
        }
        #endregion
    }
}
