using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text;

namespace WebFrontend.Pages
{
    public class HeadersModel : PageModel
    {
        private readonly ILogger<HeadersModel> _logger;

        public string ApiHeaders { get; set; } = "** Not available **";

        private IConfiguration _configuration;
        public HeadersModel(ILogger<HeadersModel> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        public async Task OnGet()
        {
            try
            {
                HttpClient hc = new HttpClient();
                ApiHeaders = await hc.GetStringAsync($"{_configuration.GetValue<string>("ApiBackendURL")}/Headers");
            }
            catch (Exception ex)
            {
                ApiHeaders= $"Exception while calling {_configuration.GetValue<string>("ApiBackendURL")}/Headers: {ex.Message}";
                _logger.LogError(ex, ApiHeaders);
            }


        }
    }
}