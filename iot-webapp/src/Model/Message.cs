using Newtonsoft.Json;

namespace iot_webapp.Model {
    public class Message {
        public string Name { get; set; }
        public string Msg { get; set; }

        public override string ToString() {
            return JsonConvert.SerializeObject(this);
        }
    }
}