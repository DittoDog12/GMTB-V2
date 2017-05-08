using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace GMTB
{
    /// <summary>
    /// Main input detection system
    /// </summary>
    public class Input
    {
        #region Data Members
        private static Input Instance = null;
        private KeyboardState oldState;
        public event EventHandler<InputEvent> NewInput;
        public event EventHandler<InputEvent> ExitInput;
        public event EventHandler<InputEvent> SpaceInput;
        public event EventHandler<InputEvent> UseInput;
        #endregion

        #region Constructor
        private Input()
        {
            oldState = Keyboard.GetState();
        }
        public static Input getInstance
        {
            get
            {
                if (Instance == null)
                    Instance = new Input();
                return Instance;
            }
        }
        #endregion

        #region Methods
        // Input triggers
        protected virtual void OnNewInput(Keys key)
        {
            InputEvent args = new InputEvent(key);
            NewInput(this, args);
        }
        protected virtual void OnSpaceInput(Keys key)
        {
            if (SpaceInput != null)
            {
                InputEvent args = new InputEvent(key);
                SpaceInput(this, args);
            }
        }
        protected virtual void OnUse(Keys key)
        {
            if (UseInput != null)
            {
                InputEvent args = new InputEvent(key);
                UseInput(this, args);
            }
        }
        protected virtual void onEsc(Keys key)
        {
            if (ExitInput != null)
            {
                InputEvent args = new InputEvent(key);
                ExitInput(this, args);
            }
        }
        // Sub/Unsubscribers
        public void SubscribeMove(EventHandler<InputEvent> handler)
        {
            // Add event handler
            NewInput += handler;
        }
        public void Unsubscribe(EventHandler<InputEvent> handler)
        {
            // Remove event handler
            NewInput -= handler;
        }
        public void SubscribeExit(EventHandler<InputEvent> handler)
        {
            // Add event handler
            ExitInput += handler;
        }
        public void unSubscribeExit(EventHandler<InputEvent> handler)
        {
            // Add event handler
            ExitInput -= handler;
        }
        public void SubscribeSpace(EventHandler<InputEvent> handler)
        {
            SpaceInput += handler;
        }
        public void UnSubscribeSpace(EventHandler<InputEvent> handler)
        {
            SpaceInput -= handler;
        }
        public void SubscribeUse(EventHandler<InputEvent> handler)
        {
            UseInput += handler;
        }
        public void UnSubscribeUse(EventHandler<InputEvent> handler)
        {
            UseInput -= handler;
        }

        public void Update()
        {
            KeyboardState newState = Keyboard.GetState();
            // Halt input detection if the global trigger is set, usually if Dialogue is running
            if (Kernel._gameState == Kernel.GameStates.Playing)
            {


                if (newState.IsKeyDown(Keys.W) == true || GamePad.GetState(PlayerIndex.One).DPad.Up == ButtonState.Pressed)
                {
                    OnNewInput(Keys.W);
                }
                else if (newState.IsKeyDown(Keys.A) == true || GamePad.GetState(PlayerIndex.One).DPad.Left == ButtonState.Pressed)
                {
                    OnNewInput(Keys.A);
                }
                else if (newState.IsKeyDown(Keys.S) == true || GamePad.GetState(PlayerIndex.One).DPad.Down == ButtonState.Pressed)
                {
                    OnNewInput(Keys.S);
                }
                else if (newState.IsKeyDown(Keys.D) == true || GamePad.GetState(PlayerIndex.One).DPad.Right == ButtonState.Pressed)
                {
                    OnNewInput(Keys.D);
                }

                if (newState.IsKeyDown(Keys.E))
                    OnUse(Keys.E);
            }
            if (oldState.IsKeyUp(Keys.Space) && newState.IsKeyDown(Keys.Space))
                OnSpaceInput(Keys.Space);
            if (oldState.IsKeyUp(Keys.Escape) && newState.IsKeyDown(Keys.Escape))
                onEsc(Keys.Escape);

            oldState = newState;
        }
        #endregion
    }
}
