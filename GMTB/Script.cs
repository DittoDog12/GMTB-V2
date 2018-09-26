using System;
using System.IO;
using Microsoft.Xna.Framework;

namespace GMTB
{
    public class Script
    {
        /// <summary>
        /// Dialogue Script control, sends the Dialogue Box the individual lines and controls the line display delay
        /// </summary>
        #region Data Members
        private static Script Instance = null;
        private string[] Lines; // List holding the current conversation
        private string Line; // String to hold a single line of dialogue
        private int mLine; // Currently displayed line
        private bool mNextLine; // Used for spacebar control of text if implemented
        private bool DialogueRunning; // Internal boolean to control if a conversation is running
        private bool SingleDialogueRun; // Internal boolean to control a single line of dialogue

        // Line by line delay controls
        private float timer;
        private float interval;

        #endregion

        #region Constructor
        private Script()
        {
            mNextLine = false; // Used for spacebar control of text if implemented

            mLine = 0;

            // Set Line by line delay to 3 seconds.
            interval = 3000f;
            timer = 0f;
        }
        public static Script getInstance
        {
            get
            {
                if (Instance == null)
                    Instance = new Script();
                return Instance;
            }
        }
        #endregion

        #region Methods
        public void Update(GameTime gameTime)
        {
            // Display an entire conversaion
            if (DialogueRunning)
            {
                timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                DialogueBox.getInstance.Display(Lines[mLine]); // Display current line of loaded list
                // Load next line at interval
                if (timer > interval || mNextLine == true)
                {
                    mNextLine = false;
                    mLine++; // increment current line
                    timer = 0f;
                }
                if (mLine == Lines.Length)
                {
                    DialogueBox.getInstance.Display(" ");
                    DialogueRunning = false;
                    Global.GameState = Global.availGameStates.Playing;
                    Input.getInstance.UnSubscribeSpace(this.OnSpace);
                    if (Input.getInstance.CheckController())
                        Input.getInstance.UnSubscribeGPMenu(this.GP);
                }
            }
            // Display a single line of dialogue   
            if (SingleDialogueRun)
            {
                timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                Input.getInstance.SubscribeSpace(OnSpace);
                if (Input.getInstance.CheckController())
                    Input.getInstance.SubscribeGPMenu(this.GP);
                DialogueBox.getInstance.Display(Line);
                if (timer > interval || mNextLine == true)
                {
                    mNextLine = false;
                    DialogueBox.getInstance.Display(" ");
                    Global.GameState = Global.availGameStates.Playing;
                    SingleDialogueRun = false;
                    timer = 0f;
                }
            }

        }
        // Used to move the dialogue forwards on pressing the space bar, currenly skips several lines
        public void OnSpace(object source, InputEvent args)
        {
            mNextLine = true;
        }

        // Called by Collision triggers to start a conversaion
        public void BeginDialogue(string[] lines)
        {
            Lines = lines;
            DialogueRunning = true;
            mLine = 0;
            Global.GameState = Global.availGameStates.Dialogue;
            Input.getInstance.SubscribeSpace(this.OnSpace);
            if (Input.getInstance.CheckController())
                Input.getInstance.SubscribeGPMenu(this.GP);
        }

        // Called by Collision triggers to display one line of dialogue
        public void SingleDialogue(string line)
        {
            Line = line;
            SingleDialogueRun = true;
            Global.GameState = Global.availGameStates.Dialogue;
        }

        public void GP(object source, GPEvent args)
        {
            if (args.currentState == "A")
                mNextLine = true;
        }
        #endregion

}
}
