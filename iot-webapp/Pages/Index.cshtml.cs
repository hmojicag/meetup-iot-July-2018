using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using iot_webapp.Model;
using iot_webapp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace iot_webapp.Pages {
    public class IndexModel : PageModel {

        private IMessagesService _messagesService;
        
        [BindProperty]
        [Required(ErrorMessage = "Name is required")]
        [StringLength(16, MinimumLength = 3)]
        public string Name { get; set; }
        
        [BindProperty]
        [Required(ErrorMessage = "Message is required")]
        [StringLength(16, MinimumLength = 3)]
        public string Message { get; set; }

        public IndexModel(IMessagesService _messagesService) {
            this._messagesService = _messagesService;
        }
        
        public IActionResult OnGet() {
            return Page();
        }

        public IActionResult OnPost() {
            if (!ModelState.IsValid) {
                return Page();
            }

            var msg = new Message {
                Name = Name,
                Msg = Message
            };
            
            _messagesService.AddMessage(msg);
            
            return Page();
        }
    }
}
