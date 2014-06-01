namespace SSSG.Input
{
    using Microsoft.Xna.Framework.Input;

    public interface IKeyboardInput
    {
        bool IsKeyDown(Keys key);
        bool IsKeyUp(Keys key);
    }
}