using System;

namespace GMTB
{
    /// <summary>
    /// Event for triggering if the player is in close proximity to an object
    /// </summary>
    public class ProximityEvent : EventArgs
    {
        public hasProximity Entity;
        public ProximityEvent(hasProximity entity)
        {
            Entity = entity;
        }

    }
}
