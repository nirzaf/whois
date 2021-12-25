using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.ComponentModel.DataAnnotations;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace webquery.Pages
{
    public class Query
    {
        [Required, StringLength(200)]
        public string WhoisQuery { get; set; }

        public string Result { get; set; }
    }

    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        [BindProperty]
        public Query Query { get; set; }

        public void OnGet()
        {
            Query = new Query();
        }

        public async Task OnPostAsync()
        {
            using var cert2 = new X509Certificate2(Environment.GetEnvironmentVariable("ASPNETCORE_Kestrel__Certificates__Default__Path"),
                Environment.GetEnvironmentVariable("ASPNETCORE_Kestrel__Certificates__Default__Password"));

            var handler = new HttpClientHandler();
            handler.ClientCertificates.Add(cert2);
            handler.ClientCertificateOptions = ClientCertificateOption.Manual;
            var httpClient = new HttpClient(handler);

            using var resp = await httpClient.GetAsync($"https://whoisit/whois/{Query.WhoisQuery}");
            Query.Result = await resp.Content.ReadAsStringAsync();
        }
    }
}
