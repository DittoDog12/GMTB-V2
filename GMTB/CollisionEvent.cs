using System;

namespace GMTB
{
    public class CollisionEvent : EventArgs
    {
        public Collidable Entity;
        public IWall Wall;
        public CollisionEvent(Collidable entity)
        {
            Entity = entity;
        }
        public CollisionEvent(Collidable entity, IWall wall)
        {
            Wall = wall;
            Entity = entity;
        }
        public CollisionEvent()
        {

        }
    }
}
