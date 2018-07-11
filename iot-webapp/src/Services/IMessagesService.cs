using System.Collections;
using iot_webapp.Model;

namespace iot_webapp.Services {
    public interface IMessagesService {
        void AddMessage(Message message);
        IEnumerable GetMessages();
        string GetIoTMessages();
    }
}