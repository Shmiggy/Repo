namespace SSSG.Input
{
    using SSSG.Utils.Patterns;

    public interface IKeyboardInput : IObserver
    {
        event KeyboardHandler KeyPressed;
        event KeyboardHandler KeyReleased;
    }
}