namespace SSSG.Input
{
    using Microsoft.Xna.Framework.Input;

    public interface IKeyboardInput
    {
        /// <summary>
        /// Returns whether or not the target key is down.
        /// </summary>
        /// <param name="key">the target key</param>
        /// <returns></returns>
        bool IsKeyDown(Keys key);

        /// <summary>
        /// Returns whether or not the target key is up.
        /// </summary>
        /// <param name="key">the target key</param>
        /// <returns></returns>
        bool IsKeyUp(Keys key);
    }
}