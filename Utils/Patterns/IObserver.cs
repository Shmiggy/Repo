namespace SSSG.Utils.Patterns
{
    public interface IObserver
    {
        void Update (ISubject subject, object payload);
    }
}
