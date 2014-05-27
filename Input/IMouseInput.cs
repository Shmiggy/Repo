namespace SSSG.Input
{
    using SSSG.Utils.Patterns;

    public interface IMouseInput : IObserver
    {
        event MouseMovedHandler MouseMoved;
        event MouseHandler LeftButtonDown;
    }
}