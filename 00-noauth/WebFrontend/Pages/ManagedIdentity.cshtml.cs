using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Reflection.PortableExecutable;

namespace WebFrontend.Pages
{
    public class ManagedIdentityModel : PageModel
    {
        private readonly ILogger<ManagedIdentityModel> _logger;

        public bool HasManagedIdentity { get; set; } = false;
        public string ManagedIdentityInfo { get; set; } = "";
        public string ManagedIdentityEndpoint { get; set; } = ""; // format is "http://127.0.0.1:41252/msi/token/"

        public string ResourceUri { get; set; } = "";

        readonly IConfiguration _configuration;

        public ManagedIdentityModel(ILogger<ManagedIdentityModel> logger,IConfiguration configuration)
        {
            _logger = logger;
            this._configuration = configuration;
        }

        public async Task OnGet()
        {
            if (string.IsNullOrEmpty(System.Environment.GetEnvironmentVariable("IDENTITY_ENDPOINT")))
            {
                this.HasManagedIdentity = false;
                this.ManagedIdentityInfo="{\n \"Error\" : \"Not defined\" }";
            }
            else
            {
                this.HasManagedIdentity = true;
                this.ManagedIdentityEndpoint = System.Environment.GetEnvironmentVariable("IDENTITY_ENDPOINT");
                this.ManagedIdentityInfo = "TO RETRIEVE ....";
                // call private endpoint to retrieve identity info
                this.ResourceUri = _configuration.GetValue<string>("ResourceUri");
                if (string.IsNullOrEmpty(ResourceUri))
                {
                    this.ManagedIdentityInfo = "No ResourceUri. Please configure the 'ResourceUri' settings.";
                }
                else {
                    var url = $"{ManagedIdentityEndpoint}?resource={ResourceUri}&api-version=2019-08-01";
                    try
                    {
                        // GET /MSI/token?resource=https://XXXXXX.azure.net&api-version=2019-08-01 HTTP/1.1
                        HttpClient hc = new HttpClient();
                        hc.DefaultRequestHeaders.Add("X-IDENTITY-HEADER", Environment.GetEnvironmentVariable("IDENTITY_HEADER"));
                        ManagedIdentityInfo = await hc.GetStringAsync(url);
                        // or use .Net package Azure.Identity :)
                    }
                    catch (Exception ex)
                    {
                        ManagedIdentityInfo =
                            "URL = " + url + "\n" + 
                            "EXCEPTION : " + ex.Message + "\n\n" + ex.ToString();
                    }
                }
            }

        }
    }
}