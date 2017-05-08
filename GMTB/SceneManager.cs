using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GMTB
{
    public class SceneManager
    {
        /// <summary>
        /// Main Scene Manager, places everything on the screen and calls Draw methods
        /// </summary>
        #region Data Members
        private static SceneManager Instance = null;

        private List<IEntity> mEntities;
        private List<IEntity> mSceneGraph;
        Microsoft.Xna.Framework.Content.ContentManager Content;
        #endregion

        #region Accessors
        public List<IEntity> Entities
        {
            get { return mEntities; }
        }

        public List<IEntity> SceneGraph
        {
            get { return mSceneGraph; }
        }
        #endregion

        #region Constructor
        private SceneManager()
        {
            // Initialise Entity List
            mEntities = EntityManager.getInstance.Entities;
            mSceneGraph = new List<IEntity>();
            Content = Global.Content;
        }
        public static SceneManager getInstance
        {
            get
            {
                if (Instance == null)
                    Instance = new SceneManager();
                return Instance;
            }
        }
        #endregion

        #region Methods
        //public void load()
        //{


        //    Kernel._gameState = Kernel.GameStates.Loading;
        //}
        //public void Save()
        //{
        //    // Open Storage Container
        //    IAsyncResult result = device.BeginOpenContainer("StorageDevice", null, null);
        //    result.AsyncWaitHandle.WaitOne();

            
        //}

        public void newEntity(IEntity createdEntity, int x, int y)
        {
            // Add the new entity to the SceneManagers entity list
            //mEntities.Add(createdEntity);

            mSceneGraph.Add(createdEntity);
            // Apply the entities texture
            mEntities.ForEach(IEntity => IEntity.aTexture = Content.Load<Texture2D>(IEntity.aTexturename));
            // Set the entities initial position
            createdEntity.setPos(x, y);
            createdEntity.DefaultPos = new Vector2(x, y);

        }
        public void Update(GameTime gameTime)
        {
            if (Kernel._gameState == Kernel.GameStates.Playing)
                mEntities.ForEach(IEntity => IEntity.Update(gameTime));
            if (Kernel._gameState == Kernel.GameStates.Paused)
                Kernel.menu2.Update(gameTime);
            if (Kernel._gameState == Kernel.GameStates.GameOver)
                Kernel.GameOverScreen.Update(gameTime);

        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            RoomManager.getInstance.Draw(spriteBatch);

            // Only run draw if not in Game Over state
            if (Kernel._gameState != Kernel.GameStates.GameOver)
            {
                // Update Texture path for animating entity
                mEntities.ForEach(IEntity => IEntity.aTexture = Content.Load<Texture2D>(IEntity.aTexturename));
                // Call draw method for each Entity if entity is visible
                for (int i = 0; i < mEntities.Count; i++)
                    if (mEntities[i].Visible)
                        mEntities[i].Draw(spriteBatch);
                // Run Dialogue display if game in Dialogue State
                if (Kernel._gameState == Kernel.GameStates.Dialogue)
                    DialogueBox.getInstance.Draw(spriteBatch);
                if (Kernel._gameState == Kernel.GameStates.Paused)
                    if (Kernel.menu2 != null)
                        Kernel.menu2.Draw(spriteBatch);

            }
            else if (Kernel.GameOverScreen != null && Kernel._gameState == Kernel.GameStates.GameOver)
                Kernel.GameOverScreen.Draw(spriteBatch);

            spriteBatch.End();
        }
        #endregion
    }
}
