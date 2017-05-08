using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GMTB
{
    /// <summary>
    /// Main Entity interface
    /// </summary>
    public interface IEntity
    {
        #region Accessors
        int UID { get; set; }
        string UName { get; }
        Texture2D aTexture { set; }
        string aTexturename { get; }
        Vector2 Position { get; }
        bool Collidable { get; }
        bool Visible { get; }
        Vector2 DefaultPos { get; set; }
        #endregion

        #region Methods
        void setVars(int uid);
        void setVars(int uid, string path);
        void setVars(string path, bool Dialogue);
        void setVars(int uid, PlayerIndex pPlayer);
        void setVars(string tRoom, Vector2 playerStart);

        void setPos(int x, int y);
        void Update(GameTime gameTime);
        void Draw(SpriteBatch spriteBatch);
        void Destroy();
        void sub();
        #endregion
    }
}
