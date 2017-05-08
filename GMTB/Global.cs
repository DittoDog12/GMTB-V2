using Microsoft.Xna.Framework;

namespace GMTB
{
    /// <summary>
    /// Contains global variables
    /// </summary>
    public static class Global
    {
        /// <summary>
        /// Static Boolean to pause all activity while a dialouge box is running
        /// </summary>
        static bool _PauseInput;

        /// <summary>
        /// Static reference to trigger game over state
        /// </summary>
        //static bool _GameOver = false;

        /// <summary>
        /// Static reference to the Content Manager
        /// </summary>
        static Microsoft.Xna.Framework.Content.ContentManager _Content;

        /// <summary>
        /// Static referecne to the players position
        /// </summary>
        static Vector2 _PlayerPos;

        /// <summary>
        /// Static Reference to the Player, used for doors
        /// </summary>
        static IPlayer _player;

        /// <summary>
        /// Accessor for _PauseInput
        /// </summary>
        public static bool PauseInput
        {
            get { return _PauseInput; }
            set { _PauseInput = value; }
        }

        /// <summary>
        /// Accessor for _GameOver
        /// </summary>
        //public static bool GameOver
        //{
        //    get { return _GameOver; }
        //    set { _GameOver = value; }
        //}

        /// <summary>
        /// Accessor for _Content
        /// </summary>
        public static Microsoft.Xna.Framework.Content.ContentManager Content
        {
            get { return _Content; }
            set { _Content = value; }
        }
        /// <summary>
        /// Accessor for _PlayerPos
        /// </summary>
        public static Vector2 PlayerPos
        {
            get { return _PlayerPos; }
            set { _PlayerPos = value; }
        }

        /// <summary>
        /// Accessor for _player
        /// </summary>
        public static IPlayer Player
        {
            get { return _player; }
            set { _player = value; }
        }
    }
}
