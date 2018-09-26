using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace GMTB
{
    #region Input Event
    /// <summary>
    /// Input Event mini public class
    /// </summary> 
    public class InputEvent : EventArgs
    {
        public Keys currentKey;

        public InputEvent(Keys key)
        {
            currentKey = key;
        }
    }

    public class GPEvent : EventArgs
    {
        public string currentState;

        public GPEvent(string state)
        {
            currentState = state;
        }
    }
    #endregion

    /// <summary>
    /// Main input detection system
    /// </summary>
    public class Input
    {
        #region Data Members
        private static Input Instance = null;
        private KeyboardState oldState;
        private GamePadState oldGstate;
        public event EventHandler<InputEvent> NewInput;
        public event EventHandler<InputEvent> ExitInput;
        public event EventHandler<InputEvent> SpaceInput;
        public event EventHandler<InputEvent> UseInput;

        public event EventHandler<GPEvent> GPMenu;
        private bool gpCon;
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
        #region Event Triggers
        protected virtual void OnNewInput(Keys key)
        {
            InputEvent args = new InputEvent(key);
            NewInput(this, args);
        }
        //protected virtual void HorizontalInput(Keys key)
        //{
        //    InputEvent args = new InputEvent(key);
        //    NewInput(this, args);
        //}
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
        protected virtual void onGPInput(string gp)
        {
            if (gp != null)
            {
                GPEvent args = new GPEvent(gp);
                GPMenu(this, args);
            }
        }
        #endregion
        #region Sub/Unsubscribers
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

        public void SubscribeGPMenu(EventHandler<GPEvent> handler)
        {
            GPMenu += handler;
        }
        public void UnSubscribeGPMenu(EventHandler<GPEvent> handler)
        {
            GPMenu -= handler;
        }
        #endregion
        public bool CheckController()
        {
            gpCon = false;
            if (GamePad.GetState(PlayerIndex.One).IsConnected == true)
                gpCon = true;
            return gpCon;
        }

        public void Update()
        {
            if (CheckController() == true)
                GamePadInput();

            KeyboardState newState = Keyboard.GetState();
            // Halt input detection if the global trigger is set, usually if Dialogue is running
            if (Global.GameState == Global.availGameStates.Playing)
            {


                if (newState.IsKeyDown(Keys.W) == true || GamePad.GetState(PlayerIndex.One).DPad.Up == ButtonState.Pressed)
                {
                    OnNewInput(Keys.W); //FIX THIS ON SIDSCROLL
                }
                
                else if (newState.IsKeyDown(Keys.S) == true || GamePad.GetState(PlayerIndex.One).DPad.Down == ButtonState.Pressed)
                {
                    OnNewInput(Keys.S);
                }

                if (newState.IsKeyDown(Keys.A) == true || GamePad.GetState(PlayerIndex.One).DPad.Left == ButtonState.Pressed)
                {
                    OnNewInput(Keys.A);
                }
                else if (newState.IsKeyDown(Keys.D) == true || GamePad.GetState(PlayerIndex.One).DPad.Right == ButtonState.Pressed)
                {
                    OnNewInput(Keys.D);
                }

                if (oldState.IsKeyUp(Keys.E) && newState.IsKeyDown(Keys.E))
                    OnUse(Keys.E);
            }
            if (oldState.IsKeyUp(Keys.Space) && newState.IsKeyDown(Keys.Space))
                OnSpaceInput(Keys.Space);
            if (oldState.IsKeyUp(Keys.Escape) && newState.IsKeyDown(Keys.Escape))
                onEsc(Keys.Escape);

            oldState = newState;
        }

        private void GamePadInput()
        {
            GamePadState newGstate = GamePad.GetState(PlayerIndex.One);

            if (Global.GameState == Global.availGameStates.Playing)
            {
                if (newGstate.DPad.Up == ButtonState.Pressed || newGstate.ThumbSticks.Left.Y > 0)
                {
                    OnNewInput(Keys.W);
                }

                else if (newGstate.DPad.Down == ButtonState.Pressed || newGstate.ThumbSticks.Left.Y < 0)
                {
                    OnNewInput(Keys.S);
                }

                if (newGstate.DPad.Left == ButtonState.Pressed || newGstate.ThumbSticks.Left.X < 0)
                {
                    OnNewInput(Keys.A);
                }
                else if (newGstate.DPad.Right == ButtonState.Pressed || newGstate.ThumbSticks.Left.X > 0)
                {
                    OnNewInput(Keys.D);
                }

                if (newGstate.Buttons.A == ButtonState.Pressed && oldGstate.Buttons.A == ButtonState.Released)
                    OnSpaceInput(Keys.Space);
            }

            if (newGstate.Buttons.Start == ButtonState.Released && oldGstate.Buttons.Start == ButtonState.Pressed)
                onEsc(Keys.Escape);

            if (Global.GameState == Global.availGameStates.Menu || Global.GameState == Global.availGameStates.Paused 
                || Global.GameState == Global.availGameStates.Dialogue || Global.GameState == Global.availGameStates.GameOver)
            {
                if (newGstate.Buttons.A == ButtonState.Released && oldGstate.Buttons.A == ButtonState.Pressed)
                {
                    string state = "A";
                    onGPInput(state);
                }
                if (newGstate.Buttons.B == ButtonState.Released && oldGstate.Buttons.B == ButtonState.Pressed)
                {
                    string state = "B";
                    onGPInput(state);
                }
                if (newGstate.Buttons.X == ButtonState.Released && oldGstate.Buttons.X == ButtonState.Pressed)
                {
                    string state = "X";
                    onGPInput(state);
                }

            }

            oldGstate = newGstate;
        }
        #endregion
    }
}
