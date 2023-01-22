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


        public EnvVariablesModel(ILogger<EnvVariablesModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
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

            // TODO : call API 
            //try
            //{
            //    _logger.LogInformation($"TodoServiceUri={TodoServiceUri}");
            //    HttpClient hc = new HttpClient();
            //    ApiEnvironmentVariables = await hc.GetStringAsync($"{TodoServiceUri}/api/Help/Env");
            //}
            //catch (Exception ex)
            //{
            //    ApiEnvironmentVariables = $"Exception while calling {TodoServiceUri}/api/Help/Env : {ex.Message}";
            //    _logger.LogError(ex, ApiEnvironmentVariables);
            //}


        }
    }
}