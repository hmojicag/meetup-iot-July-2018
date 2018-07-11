using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using iot_webapp.Model;

namespace iot_webapp.Services {
    public class MessagesService : IMessagesService {

        private Queue<Message> Messages;
        private Timer _timer;
        
        public MessagesService() {
            Messages = new Queue<Message>();
            //Tick every 30sec
            _timer = new Timer(TimerTask, null, 1000, 30000); 
        }

        public void AddMessage(Message message) {
            if (Messages.Count >= 10) {
                //Max 10 messages allowed
                Messages.Dequeue();
            }
            Messages.Enqueue(message);
        }

        public IEnumerable GetMessages() {
            return Messages;
        }

        public string GetIoTMessages() {
            var builder = new StringBuilder();
            foreach (var msg in Messages) {
                builder.Append(msg.Name)
                    .Append(":")
                    .Append(msg.Msg)
                    .Append(";");
            }
            return builder.ToString();
        }

        private void TimerTask(object timerState) {
            if (Messages.Count > 0) {
                //Delete one message
                Console.WriteLine(Messages.Dequeue());
            }
        }
        
    }
}