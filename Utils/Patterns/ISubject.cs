namespace SSSG.Utils.Patterns
{
    public interface ISubject
    {
        void AttachObserver(IObserver observer);
        void DetachObserver(IObserver observer);
        void Notify(object payload);
    }
}