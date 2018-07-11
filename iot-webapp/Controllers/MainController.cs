using iot_webapp.Services;
using Microsoft.AspNetCore.Mvc;

namespace iot_webapp.Controllers {
    
    [Route("api")]
    public class MainController {
        private readonly IMessagesService _messagesService;
        private readonly ITemperatureService _temperatureService;

        public MainController(IMessagesService _messagesService, ITemperatureService _temperatureService) {
            this._messagesService = _messagesService;
            this._temperatureService = _temperatureService;
        }

        [HttpGet("json-msg")]
        //GET /api/json-msg
        public IActionResult GetJsonMessages() {
            return new JsonResult(_messagesService.GetMessages());
        }
        
        [HttpGet("iot-msg")]
        //GET /api/json-msg
        public ContentResult GetIoTMessages() {
            return new ContentResult() {
                Content = _messagesService.GetIoTMessages()
            };
        }

        [HttpPost("temp")]
        //POST /api/temp
        public IActionResult PostTemperature(double temperature) {
            _temperatureService.SetTemperature(temperature);
            return new OkResult();
        }
        
        [HttpGet("temp")]
        //GET /api/temp
        public IActionResult GetTemperature() {
            return new JsonResult(_temperatureService.Temperature);
        }
        
        
        
    }
}