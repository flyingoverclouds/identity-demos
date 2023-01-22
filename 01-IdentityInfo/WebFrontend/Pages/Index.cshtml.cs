using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebFrontend.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        private IConfiguration _configuration;

        public string ApiBackendUrl {  get; set; }
        public IndexModel(ILogger<IndexModel> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;

            this.ApiBackendUrl = _configuration.GetValue<string>("ApiBackendURL");
        }

        public void OnGet()
        {

        }
    }
}