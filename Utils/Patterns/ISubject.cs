namespace SSSG.Utils.Patterns
{
    public interface ISubject
    {
        /// <summary>
        /// Attaches an observer.
        /// </summary>
        /// <param name="observer">observer to be attached</param>
        void AttachObserver(IObserver observer);

        /// <summary>
        /// Detaches an observer.
        /// </summary>
        /// <param name="observer">observer to be detached</param>
        void DetachObserver(IObserver observer);

        /// <summary>
        /// Notify all observers.
        /// </summary>
        /// <param name="payload">data to be sent</param>
        void Notify(object payload);
    }
}