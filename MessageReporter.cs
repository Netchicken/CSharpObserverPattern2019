using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpObserverPattern2019
{//Observers register to receive notifications from a MessageLocation object by calling its IObservable<T>.Subscribe method, which assigns a reference to the observer object to a private generic List<T> object.
 //The method returns an Unsubscriber object, which is an IDisposable implementation that enables observers to stop receiving notifications.
 //The MessageTracker class also includes an EndTransmission method. When no further location data is available, the method calls each observer's OnCompleted method and then clears the internal list of observers.

    // In this example, the MessageReporter class provides the IObserver<T> implementation.
    // It displays information about the current message to the console.
    // Its constructor includes a name parameter, which enables the MessageReporter instance to identify itself in its string output.
    // It also includes a Subscribe method, which wraps a call to the provider's Subscribe method. This allows the method to assign the returned IDisposable reference to a private variable.
    // The MessageReporter class also includes an Unsubscribe method, which calls the IDisposable.Dispose method of the object that is returned by the IObservable<T>.Subscribe method. The following code defines the MessageReporter class.
    class MessageReporter : IObserver<Message>
    {
        private IDisposable unsubscriber;
        public string CurrentMessage; //just takes the message and makes it available to the reporter
        public MessageReporter(string name)
        {
            this.Name = name;
        }
        /// <summary>
        /// Name of hte Reporter Facebook, Instagram
        /// </summary>
        public string Name { get; }

        public virtual void Subscribe(IObservable<Message> provider)
        {
            if (provider != null)
                unsubscriber = provider.Subscribe(this);
        }

        public virtual void OnCompleted()
        {
            Console.WriteLine("The Message Tracker has completed transmitting data to {0}.", this.Name);
            this.Unsubscribe();
        }

        public virtual void OnError(Exception error)
        {
            Console.WriteLine("{0}: The message cannot be determined.", this.Name);
        }
        /// <summary>
        /// Runs when a message is received
        /// </summary>
        /// <param name="value"></param>
        public virtual void OnNext(Message value)
        {
            //  Console.WriteLine("{2}: The current location is {0}, {1}", value.Latitude, value.Longitude, this.Name);
            CurrentMessage = value.Messages;
        }


        /// <summary>
        /// Stop receiving messages
        /// </summary>
        public virtual void Unsubscribe()
        {
            unsubscriber.Dispose();
        }
    }
}

