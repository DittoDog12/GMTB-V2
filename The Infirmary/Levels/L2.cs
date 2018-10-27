using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GMTB;
using GMTB.AI;
using GMTB.PlatformerSubsys;

namespace The_Infirmary.Levels
{
    class L2 : Level
    {
        #region Constructor
        public L2() :base()
        {
            bg = null;
        }
        #endregion
        #region Methods
        public override void Initialise()
        {
            if (firstRun == true)
            {
                //Martha
                createdEntity = EntityManager.getInstance.newEntity<HighLevelAI>("Enemy/Martha/");
                SceneManager.getInstance.newEntity(createdEntity, 40, 350);
                var asInstance = createdEntity as IAI;
                asInstance.setVars(true, "Idle");
                Removables.Add(createdEntity);

                createdEntity = EntityManager.getInstance.newEntity<SolidGround>("Block");
                SceneManager.getInstance.newEntity(createdEntity, -100, 400);
                Removables.Add(createdEntity);

                createdEntity = EntityManager.getInstance.newEntity<SolidGround>("Block");
                SceneManager.getInstance.newEntity(createdEntity, 700, 330);
                Removables.Add(createdEntity);

                firstRun = false;
            }
        }
        #endregion
    }
}
