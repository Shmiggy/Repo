namespace SSSG.Utils.Patterns
{
    public interface IObserver
    {
        /// <summary>
        /// Updates the current observer.
        /// </summary>
        /// <param name="subject">the sender</param>
        /// <param name="payload">data sent</param>
        void Update (ISubject subject, object payload);
    }
}
