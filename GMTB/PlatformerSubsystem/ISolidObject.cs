using Microsoft.Xna.Framework;

namespace GMTB.PlatformerSubsys
{
    public interface ISolidObject
    {
        Rectangle ProximityBox { get; }
    }
}
