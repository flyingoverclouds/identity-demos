using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text;

namespace WebFrontend.Pages
{
    public class EnvVariablesModel : PageModel
    {
        private readonly ILogger<EnvVariablesModel> _logger;
        public string ApiEnvironmentVariables { get; set; } = "";
        public string FrontEnvironmentVariables { get; set; }

        private IConfiguration _configuration;

        public EnvVariablesModel(ILogger<EnvVariablesModel> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        public async Task OnGet()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var evname in Environment.GetEnvironmentVariables().Keys)
            {
                var evValue = Environment.GetEnvironmentVariable(evname.ToString());
                if (evValue.EndsWith("==") || evname.ToString().ToLower().Contains("key"))
                    evValue = evValue.Substring(0, Math.Min(evValue.Length, 5)) + "**REDACTED**";
                sb.AppendLine($"{evname.ToString()}={evValue}");
            }
            FrontEnvironmentVariables = sb.ToString();

            try
            {
                HttpClient hc = new HttpClient();
                ApiEnvironmentVariables = await hc.GetStringAsync($"{_configuration.GetValue<string>("ApiBackendURL")}/EnvVariables");
            }
            catch (Exception ex)
            {
                ApiEnvironmentVariables = $"Exception while calling {_configuration.GetValue<string>("ApiBackendURL")}/EnvVariables : {ex.Message}";
                _logger.LogError(ex, ApiEnvironmentVariables);
            }


        }
    }
}