using Microsoft.Xna.Framework;

namespace GMTB
{
    /// <summary>
    /// Collidable interface, implemented by any entities with collisions
    /// </summary>
    public interface Collidable
    {
        Rectangle HitBox { get; }
        string UName { get; }
        void Collision(object source, CollisionEvent args);
    }
}
