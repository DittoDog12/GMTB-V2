using Microsoft.Xna.Framework;

namespace GMTB
{
    /// <summary>
    /// Dialogue Script specific Interface
    /// </summary>
    public interface IScript
    {
        void BeginDialogue(string[] Lines);
        void SingleDialogue(string line);
        void Update(GameTime gameTime);
    }
}
