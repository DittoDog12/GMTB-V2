using Microsoft.Xna.Framework;
using System.IO;
using System;

namespace GMTB
{
    /// <summary>
    /// Object the player can hide in, will stop the AI from tracking the player
    /// </summary>
    class HidingPlace : Entity, Collidable, hasProximity
    {
        #region Data Members
        private bool Hide;
        private bool TriggerDialouge = false;
        private string[] Lines;
        private bool runonce = false;
        // Second Hitbox for proximity detection
        // Make the box twice as big as the texture and off set it by minus half the texture size only on X axis
        public virtual Rectangle ProximityBox
        {
            get { return new Rectangle((int)mPosition.X - mTexture.Width / 2, (int)mPosition.Y, 
                mTexture.Width *2, mTexture.Height * 2); }
        }
        #endregion

        #region Constructor
        public HidingPlace()
        {
           // mTexturename = "AdultsBedLong";
            mUName = "HidingPlace";
            Hide = false;
            Input.getInstance.SubscribeUse(OnUse);
            CollisionManager.getInstance.Subscribe(Collision, this);
            ProximityManager.getInstance.Subscribe(inProximity, this);
        }
        #endregion

        #region Methods
        public override void setVars(string path, bool Dialogue)
        {
            TriggerDialouge = Dialogue;
            if (TriggerDialouge == true)
                Lines = File.ReadAllLines(Environment.CurrentDirectory + path);
        }
        public void inProximity(object source, ProximityEvent args)
        {
            if (args.Entity == this)
            {
                if (Hide)
                {
                    CollisionManager.getInstance.currPlayer.Visible = false;
                    Hide = false;
                    if (TriggerDialouge == true)
                        if (!runonce)
                        {
                            Script.getInstance.BeginDialogue(Lines);
                            runonce = true;
                        }
                            
                }
            }
               
        }
        public void OnUse(object source, InputEvent args)
        {
            Hide = true;
        }
        public override void Destroy()
        {
            base.Destroy();
            CollisionManager.getInstance.unSubscribe(Collision, this);
            ProximityManager.getInstance.unSubscribe(inProximity, this);

        }
        public override void sub()
        {
            CollisionManager.getInstance.Subscribe(Collision, this);
            ProximityManager.getInstance.Subscribe(inProximity, this);
        }
        #endregion
    }
}
