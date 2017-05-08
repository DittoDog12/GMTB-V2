using System.Collections.Generic;
using Microsoft.Xna.Framework;
using GMTB.AI;


namespace GMTB.Content.Levels
{
    class L3 : Level
    {
        #region Constructor
        public L3() : base()
        {
            bg = "Backgrounds\\MatronsOfficeBackground";
        }
        #endregion

        #region Methods
        public override void Initialise()
        {
            if (firstRun == true)
            {
                //Martha
                createdEntity = EntityManager.getInstance.newEntity<HighLevelAI>("Enemy/Martha/");
                SceneManager.getInstance.newEntity(createdEntity, 550, ScreenHeight / 2);
                var asInstance = createdEntity as IAI;
                asInstance.setVars(true, "Wait");
                Removables.Add(createdEntity);

                // Chair
                createdEntity = EntityManager.getInstance.newEntity<SolidObject>("DeskChairSide");
                SceneManager.getInstance.newEntity(createdEntity, 275, (ScreenHeight / 2) + 5);
                Removables.Add(createdEntity);
                // Desk
                createdEntity = EntityManager.getInstance.newEntity<SolidObject>("Desk");
                SceneManager.getInstance.newEntity(createdEntity, 300, ScreenHeight / 2);
                Removables.Add(createdEntity);

                // Cupboard
                createdEntity = EntityManager.getInstance.newEntity<HidingPlace>("Game Items/Wardrope");
                SceneManager.getInstance.newEntity(createdEntity, 500, 125);
                createdEntity.setVars("/Content/Dialogue/A.I.AndSerena.txt", true);
                Removables.Add(createdEntity);

                // Door
                createdEntity = EntityManager.getInstance.newEntity<Door>();
                SceneManager.getInstance.newEntity(createdEntity, 590, ScreenHeight / 2);
                createdEntity.setVars("L2", new Vector2(320, 300));
                Removables.Add(createdEntity);


                //// Walls
                ////Left
                //wall = new InvisibleWall();
                //wall.setVars(new Vector2(200, 125), new Vector2(10, 275));
                //Walls.Add(wall);
                ////right
                //wall = new InvisibleWall();
                //wall.setVars(new Vector2(620, 125), new Vector2(10, 275));
                //Walls.Add(wall);
                ////top
                //wall = new InvisibleWall();
                //wall.setVars(new Vector2(200, 125), new Vector2(420, 10));
                //Walls.Add(wall);
                ////bottom
                //wall = new InvisibleWall();
                //wall.setVars(new Vector2(200, 400), new Vector2(420, 10));
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
