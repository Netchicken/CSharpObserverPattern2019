using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//https://docs.microsoft.com/en-us/dotnet/api/system.iobservable-1?view=netframework-4.7.2

namespace CSharpObserverPattern2019
{
    class Message
    {
        //constructor takes in the message
        public Message(string message)
        {
            //pass it tp private variable
            Messages = message;
        }
        //use the Messages Property
        public string Messages { get; }
    }
}
