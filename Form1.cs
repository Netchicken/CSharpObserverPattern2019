using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CSharpObserverPattern2019
{
    public partial class Form1 : Form
    {
        private int count = 0;

        MessageTracker provider = new MessageTracker();

        //Instantiate new reporters to follow the messages
        MessageReporter reporter1 = new MessageReporter("FaceBook");
        MessageReporter reporter2 = new MessageReporter("Instagram");
        MessageReporter reporter3 = new MessageReporter("Google");







        public Form1()
        {
            InitializeComponent();
            txtheading.Text = "This pattern is used when there is one to many relationship between objects. " + Environment.NewLine + "If one object is modified, its dependent objects are to be notified automatically. " + Environment.NewLine + "This pattern is allows a single object, known as the subject, to publish changes to its state. " + Environment.NewLine + " Other observer objects that depend upon the subject are automatically notified of any changes to the subject's state.";


            reporter1.Subscribe(provider);
            reporter2.Subscribe(provider);
            reporter3.Subscribe(provider);

        }
        //Set it so you know if a notification never come - a default value
        //set it to record number of notifications - counter
        //check how fast notifications come, may need to be sampled or take each one

        private void BtnSubject_Click(object sender, EventArgs e)
        {
            string currentMessage = "I saw you change! " + count + " times";
            provider.TrackMessage(new Message(currentMessage));

            label1.Text = reporter1.CurrentMessage + " " + reporter1.Name;
            label2.Text = reporter2.CurrentMessage + " " + reporter2.Name;
            label3.Text = reporter3.CurrentMessage + " " + reporter3.Name;
            count++;

            reporter1.Unsubscribe(); //receives once then unsubscribes

            //stop sending when 4
            if (count == 4)
            {
                currentMessage = "No more Transmissions";
                provider.TrackMessage(new Message(currentMessage));
                provider.EndTransmission();
            }
        }
    }
}
