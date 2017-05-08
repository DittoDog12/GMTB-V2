using System;
using System.IO;
using Microsoft.Xna.Framework;

namespace GMTB.AI
{
    /// <summary>
    /// Friendly AI controller, will trigger a conversation on first collision
    /// and a single line of dialogue on subsequent collisions
    /// </summary>
    public class FriendlyAI : Entity, Collidable
    {
        #region Data Members
        private string[] lines;
        private string[] lines2;
        private bool SecondEncounter;
        private string RepeatedText;
        private string mTexturePath;
        private bool firstDialogue = false;
        #endregion

        #region Constructor
        public FriendlyAI()
        {
            SecondEncounter = false;
            RepeatedText = "*The Old Man doesn't turn around*";
            mCollidable = true;
            lines = File.ReadAllLines(Environment.CurrentDirectory + "/Content/Dialogue/OldManAndSerena.txt");
            lines2 = File.ReadAllLines(Environment.CurrentDirectory + "/Content/Dialogue/A.I.AndSerenaAfterOldMan.txt");
            CollisionManager.getInstance.Subscribe(Collision, this);
            
        }
        #endregion

        #region Methods
        public override void setVars(int uid, string path)
        {
            UID = uid;
            mTexturePath = path;
            mTexturename = mTexturePath + "Front"; 
        }

        public override void Collision(object source, CollisionEvent args)
        {
            if (args.Entity == this && args.Wall == null)
            {
                if (!SecondEncounter)
                {
                    Script.getInstance.BeginDialogue(lines);
                    SecondEncounter = true;
                }
                else
                    Script.getInstance.SingleDialogue(RepeatedText);
            }
        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (SecondEncounter == true && Kernel._gameState == Kernel.GameStates.Playing)
            {
                mTexturename = mTexturePath + "Back";
                if (firstDialogue == false)
                {
                    Script.getInstance.BeginDialogue(lines2);
                    firstDialogue = true;
                }

            }
                
        }
        public override void Destroy()
        {
            base.Destroy();
            CollisionManager.getInstance.unSubscribe(Collision, this);
        }
        public override void sub()
        {
            CollisionManager.getInstance.Subscribe(Collision, this);
        }
        #endregion
    }
}
