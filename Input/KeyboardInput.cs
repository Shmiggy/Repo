namespace SSSG.Input
{
    using Microsoft.Xna.Framework.Input;

    public class KeyboardInput : IKeyboardInput
    {
        #region IKeyboardInput Members

        public bool IsKeyDown(Keys key)
        {
            return Keyboard.GetState().IsKeyDown(key);
        }

        public bool IsKeyUp(Keys key)
        {
            return Keyboard.GetState().IsKeyDown(key);
        }

        #endregion
    }
}
