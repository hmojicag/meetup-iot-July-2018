using iot_webapp.Services;
using Microsoft.AspNetCore.Mvc;

namespace iot_webapp.Controllers {
    
    [Route("api")]
    public class MainController {
        private readonly IMessagesService _messagesService;

        public MainController(IMessagesService _messagesService) {
            this._messagesService = _messagesService;
        }

        [HttpGet("json-msg")]
        //GET /api/json-msg
        public IActionResult GetJsonMessages() {
            return new JsonResult(_messagesService.GetMessages());
        }
        
        [HttpGet("json-iot")]
        //GET /api/json-msg
        public ContentResult GetIoTMessages() {
            return new ContentResult() {
                Content = _messagesService.GetIoTMessages()
            };
        }
        
    }
}