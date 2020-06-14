namespace Resizer.Gui.Infrastructure.Common.Messenger
{
    /// <summary>
    /// Represents the thread on which a <see cref="PubSubEvent"/> subscriber will be called
    /// </summary>
    public enum ThreadOption
    {
        /// <summary>
        /// The subscriber will be called on the same thread as the <see cref="PubSubEvent"/> was published
        /// </summary>
        PublisherThread,

        /// <summary>
        /// The subscriber will be called on the UI thread
        /// </summary>
        UIThread,

        /// <summary>
        /// The subscriber will be called on a asynchronous background thread
        /// </summary>
        BackgroundThread
    }
}
