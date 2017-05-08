using Microsoft.Xna.Framework;

namespace GMTB
{
    /// <summary>
    /// Implemented by Entities with a proximity boundry such as Hiding places
    /// </summary>
    public interface hasProximity
    {
        Rectangle ProximityBox { get; }
        void inProximity(object source, ProximityEvent args);
    }
}
