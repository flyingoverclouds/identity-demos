using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text;

namespace WebFrontend.Pages
{
    public class HeadersModel : PageModel
    {
        private readonly ILogger<HeadersModel> _logger;


        public HeadersModel(ILogger<HeadersModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            
            StringBuilder sb = new StringBuilder();
            foreach (var hname in Request.Headers.Keys)
            {
                var evValue = Request.Headers[hname];
                //if (evValue.EndsWith("==") || evname.ToString().ToLower().Contains("key"))
                //    evValue = evValue.Substring(0, Math.Min(evValue.Length, 5)) + "**REDACTED**";
                sb.AppendLine($"<b>{hname.ToString()}</b>={evValue}<br/>");
            }

        }
    }
}