using Microsoft.Xna.Framework;

namespace GMTB.AI
{
    /// <summary>
    /// AI Specific Interface
    /// </summary>
    public interface IAI
    {
        Vector2 PlayerPos { set; }
        bool PlayerVisible { set; }
        string State { get; }
        void Update(GameTime gameTime);

        void setVars(bool PatrolVert, string state);
        void setVars(bool PatrolVert, string state, string dir);
        bool CollisionChecker();
        void Idle();
    }
}
