namespace SSSG.Input
{
    using Microsoft.Xna.Framework.Input;

    public class KeyboardInput : IKeyboardInput
    {
        #region IKeyboardInput Members

        /// <summary>
        /// Returns whether or not the target key is down.
        /// </summary>
        /// <param name="key">the target key</param>
        /// <returns></returns>
        public bool IsKeyDown(Keys key)
        {
            return Keyboard.GetState().IsKeyDown(key);
        }

        /// <summary>
        /// Returns whether or not the target key is up.
        /// </summary>
        /// <param name="key">the target key</param>
        /// <returns></returns>
        public bool IsKeyUp(Keys key)
        {
            return Keyboard.GetState().IsKeyUp(key);
        }

        #endregion
    }
}
