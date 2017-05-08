using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace GMTB.Content.Levels
{
    class L5 : Level
    {
        private int[] Tablepos;
        #region Constructor
        public L5() : base()
        {
            bg = "Backgrounds\\BoardRoomBackground";
            Tablepos = new int[6];
            Tablepos[0] = 300;
            Tablepos[1] = 332;
            Tablepos[2] = 364;
            Tablepos[3] = 396;
            Tablepos[4] = 428;
            Tablepos[5] = 460;
        }
        #endregion
        #region Methods
        public override void Initialise()
        {
            if (firstRun == true)
            {
                // Table
                for (int i = 0; i < Tablepos.Length; i++)
                {
                    createdEntity = EntityManager.getInstance.newEntity<SolidObject>("Table");
                    SceneManager.getInstance.newEntity(createdEntity, Tablepos[i], (ScreenHeight / 2) - 30);
                    Removables.Add(createdEntity);
                    createdEntity = EntityManager.getInstance.newEntity<SolidObject>("Table");
                    SceneManager.getInstance.newEntity(createdEntity, Tablepos[i], (ScreenHeight / 2) - 15);
                    Removables.Add(createdEntity);
                    createdEntity = EntityManager.getInstance.newEntity<SolidObject>("Table");
                    SceneManager.getInstance.newEntity(createdEntity, Tablepos[i], (ScreenHeight / 2));
                    Removables.Add(createdEntity);
                    createdEntity = EntityManager.getInstance.newEntity<SolidObject>("Table");
                    SceneManager.getInstance.newEntity(createdEntity, Tablepos[i], (ScreenHeight / 2) + 15);
                    Removables.Add(createdEntity);
                    createdEntity = EntityManager.getInstance.newEntity<SolidObject>("Table");
                    SceneManager.getInstance.newEntity(createdEntity, Tablepos[i], (ScreenHeight / 2) + 30);
                    Removables.Add(createdEntity);
                }

                // Door - south hallway
                createdEntity = EntityManager.getInstance.newEntity<Door>();
                SceneManager.getInstance.newEntity(createdEntity, 575, ScreenHeight / 2);
                createdEntity.setVars("L4", new Vector2(100, 150));
                Removables.Add(createdEntity);

                //// Walls
                ////Left
                //wall = new InvisibleWall();
                //wall.setVars(new Vector2(190, 125), new Vector2(10, 235));
                //Walls.Add(wall);
                ////right
                //wall = new InvisibleWall();
                //wall.setVars(new Vector2(610, 125), new Vector2(10, 235));
                //Walls.Add(wall);
                ////top
                //wall = new InvisibleWall();
                //wall.setVars(new Vector2(190, 125), new Vector2(420, 10));
                //Walls.Add(wall);
                ////bottom
                //wall = new InvisibleWall();
                //wall.setVars(new Vector2(190, 360), new Vector2(420, 10));
                //Walls.Add(wall);

                ////fireplace
                //wall = new InvisibleWall();
                //wall.setVars(new Vector2(360, 135), new Vector2(75, 10));
                //Walls.Add(wall);

                firstRun = false;
            }
            else
            {
                foreach (IEntity e in EntityManager.getInstance.Entities)
                    foreach (IEntity r in Removables)
                        if (e.UID == r.UID)
                        {
                            SceneManager.getInstance.newEntity(r, (int)r.DefaultPos.X, (int)r.DefaultPos.Y);
                            r.sub();
                        }
                foreach (IWall w in Walls)
                    w.Sub();
            }
        }
        public override List<IEntity> Exit()
        {
            base.Exit();
            return Removables;
        }
        #endregion
    }
}
