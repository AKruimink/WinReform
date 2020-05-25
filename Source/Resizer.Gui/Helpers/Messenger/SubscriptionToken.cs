using System;

namespace Resizer.Gui.Helpers.Messenger
{
    /// <summary>
    /// Defines subsription token used to identify and manage a <see cref="EventSubscription"/>
    /// </summary>
    public class SubscriptionToken : IEquatable<SubscriptionToken>
    {
        /// <summary>
        /// Tokens assigned to the subscription
        /// </summary>
        private readonly Guid _token;

        /// <summary>
        /// Create a new <see cref="SubscriptionToken"/> that identifies a <see cref="PubSubEvent"/> subscription
        /// </summary>
        public SubscriptionToken()
        {
            _token = Guid.NewGuid();
        }

        /// <summary>
        /// Compair the current <see cref="SubscriptionToken"/> to another <see cref="SubscriptionToken"/>
        /// </summary>
        /// <param name="subscriptionToken">The <see cref="SubscriptionToken"/> to compair with the current <see cref="SubscriptionToken"/></param>
        /// <returns>Returns <see langword="true"/> if the <see cref="SubscriptionToken"/> is equal, otherwise it returns <see langword="false"/></returns>
        public bool Equals(SubscriptionToken subscriptionToken)
        {
            if(subscriptionToken == null)
            {
                throw new ArgumentNullException(nameof(subscriptionToken));
            }

            return Equals(_token, subscriptionToken._token);
        }

        /// <summary>
        /// Compair the current object to another object
        /// </summary>
        /// <param name="object">The <see cref="Object"/> to compair with the current <see cref="Object"/></param>
        /// <returns>Returns <see langword="true"/> if the objects are equal, otherwise it returns <see langword="false"/></returns>
        public override bool Equals(object? @object)
        {
            if(ReferenceEquals(this, @object))
            {
                return true;
            }

            if(@object is SubscriptionToken token)
            {
                return Equals(token);
            }

            return false;
        }

        /// <summary>
        /// Gets the hash code of the token
        /// </summary>
        /// <returns>Returns the hascode of the current <see cref="SubscriptionToken"/></returns>
        public override int GetHashCode() => HashCode.Combine(_token);
    }
}
