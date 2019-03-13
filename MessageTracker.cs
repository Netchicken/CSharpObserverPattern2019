using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpObserverPattern2019
{
    class MessageTracker : IObservable<Message>
    {
        public MessageTracker()
        {
            observers = new List<IObserver<Message>>();
        }


        private List<IObserver<Message>> observers;


        /// <summary>
        /// Loads up all the messageReporters that have been instantiated
        /// </summary>
        /// <param name="observer">MessageReporter reporter1 = new MessageReporter("FaceBook");</param>
        /// <returns></returns>
        public IDisposable Subscribe(IObserver<Message> observer)
        {
            if (!observers.Contains(observer))
            {
                observers.Add(observer);
            }

            return new Unsubscriber(observers, observer);
        }

        private class Unsubscriber : IDisposable
        {
            private List<IObserver<Message>> _observers;
            private IObserver<Message> _observer;

            public Unsubscriber(List<IObserver<Message>> observers, IObserver<Message> observer)
            {
                this._observers = observers;
                this._observer = observer;
            }

            public void Dispose()
            {
                if (_observer != null && _observers.Contains(_observer))
                    _observers.Remove(_observer);
            }
        }
        //The MessageTracker class provides the IObservable<T> implementation.
        //Its TrackMessage method is passed a nullable Message object that contains the message.
        //If the message value is not null, the MessageTracker method calls the OnNext method of each observer.
        public void TrackMessage(Message mess)
        {
            foreach (var observer in observers)
            {
                if (string.IsNullOrEmpty(mess.Messages))
                    observer.OnError(new MessageUnknownException());
                else
                    observer.OnNext(mess); //send the message to the reporter
            }
        }

        public void EndTransmission()
        {
            foreach (var observer in observers.ToArray())
                if (observers.Contains(observer))
                    observer.OnCompleted();

            observers.Clear();
        }
    }

    // If the Message value is null, the TrackMessage method instantiates a MessageUnknownException object, which is shown in the following example.
    // It then calls each observer's OnError method and passes it the MessageUnknownException object.
    // Note that MessageUnknownException derives from Exception, but does not add any new members.
    public class MessageUnknownException : Exception
    {
        internal MessageUnknownException()
        { }

        public MessageUnknownException(string message) : base(message)
        {
        }

        public MessageUnknownException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}

