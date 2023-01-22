using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Reflection.PortableExecutable;
using System.Security.Claims;
using System.Text;

namespace WebFrontend.Pages
{
    public class IdentityInfoModel : PageModel
    {
        private readonly ILogger<IdentityInfoModel> _logger;
       
        private IConfiguration _configuration;

        public string ApiBackendIdentityInfo { get; set; } = "** NOT AVAILAIBLE **";

        public IdentityInfoModel(ILogger<IdentityInfoModel> logger, IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task OnGet()
        {
            try
            {
                HttpClient hc = new HttpClient();
                ApiBackendIdentityInfo= await hc.GetStringAsync($"{_configuration.GetValue<string>("ApiBackendURL")}/IdentityInfo");
            }
            catch (Exception ex)
            {
                ApiBackendIdentityInfo= $"Exception while calling {_configuration.GetValue<string>("ApiBackendURL")}/IdentityInfo: {ex.Message}";
                _logger.LogError(ex, ApiBackendIdentityInfo);
            }


        }
    }
}