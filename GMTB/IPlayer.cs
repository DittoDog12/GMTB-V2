using Microsoft.Xna.Framework;

namespace GMTB
{
    /// <summary>
    /// Player specific interface
    /// </summary>
    public interface IPlayer
    {
        Rectangle HitBox { get; }
        Vector2 Position { get; set; }
        bool Visible { get; set; }
        void Collision(object source, CollisionEvent args);
        void CollisionHide();
        void setPos(Vector2 pos);
    }
}
