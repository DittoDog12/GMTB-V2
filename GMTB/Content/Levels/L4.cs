using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using GMTB.AI;

namespace GMTB.Content.Levels
{
    class L4 : Level
    {
        #region Constructor
        public L4() : base()
        {
            bg = "Backgrounds\\HallandEnterenceBackground";
        }
        #endregion
        #region Methods
        public override void Initialise()
        {
            if (firstRun == true)
            {
                // Cupboard
                createdEntity = EntityManager.getInstance.newEntity<HidingPlace>("Game Items/Wardrope");
                SceneManager.getInstance.newEntity(createdEntity, 375, 50);
                Removables.Add(createdEntity);

                // Desk
                createdEntity = EntityManager.getInstance.newEntity<SolidObject>("Desk");
                SceneManager.getInstance.newEntity(createdEntity, 425, 150);
                Removables.Add(createdEntity);

                // Nurse at Desk
                createdEntity = EntityManager.getInstance.newEntity<LowLevelAI>("Enemy/NurseA/");
                SceneManager.getInstance.newEntity(createdEntity, 400, 150);
                var asInstance = createdEntity as IAI;
                asInstance.setVars(false, "Stand", "Right");
                Removables.Add(createdEntity);
                // Nurse Patroling
                createdEntity = EntityManager.getInstance.newEntity<LowLevelAI>("Enemy/NurseB/");
                SceneManager.getInstance.newEntity(createdEntity, 100, ScreenHeight / 2);
                asInstance = createdEntity as IAI;
                asInstance.setVars(true, "Idle");
                Removables.Add(createdEntity);

                // Door - North Hallway
                createdEntity = EntityManager.getInstance.newEntity<Door>();
                SceneManager.getInstance.newEntity(createdEntity, (ScreenWidth / 4) - 10, 60);
                createdEntity.setVars("L2", new Vector2((ScreenWidth / 2) - 18, 250));
                Removables.Add(createdEntity);
                // Door - Boardroom
                createdEntity = EntityManager.getInstance.newEntity<Door>();
                SceneManager.getInstance.newEntity(createdEntity, 75, 150);
                createdEntity.setVars("L5", new Vector2(540, ScreenHeight / 2));
                Removables.Add(createdEntity);
                // Door - Ward
                createdEntity = EntityManager.getInstance.newEntity<Door>();
                SceneManager.getInstance.newEntity(createdEntity, (ScreenWidth / 4) - 10, 390);
                createdEntity.setVars("L6", new Vector2((ScreenWidth / 2) - 5, 120));
                Removables.Add(createdEntity);


                // Walls
                //Left
                wall = new InvisibleWall();
                wall.setVars(new Vector2(55, 50), new Vector2(10, 370));
                Walls.Add(wall);
                //right
                wall = new InvisibleWall();
                wall.setVars(new Vector2(725, 50), new Vector2(10, 195));
                Walls.Add(wall);
                //top left
                wall = new InvisibleWall();
                wall.setVars(new Vector2(55, 50), new Vector2(325, 10));
                Walls.Add(wall);
                //top right
                wall = new InvisibleWall();
                wall.setVars(new Vector2(350, 60), new Vector2(375, 10));
                Walls.Add(wall);
                //bottom
                wall = new InvisibleWall();
                wall.setVars(new Vector2(55, 420), new Vector2(325, 10));
                Walls.Add(wall);
                //L
                wall = new InvisibleWall();
                wall.setVars(new Vector2(350, 245), new Vector2(375, 175));
                Walls.Add(wall);

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
