using System.Net.Http;
using System.Threading.Tasks;
using IdentityModel.Client;
using Microsoft.AspNetCore.Mvc;

namespace AspNet.Authentication.IdentityServer.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Public()
        {
            var client = new HttpClient();
            var response = await client.GetAsync("https://localhost:44388/api/data/public");
            var content = await response.Content.ReadAsStringAsync();

            ViewData["Message"] = content;

            return View();
        }

        public async Task<IActionResult> Secure()
        {
            var disco = await DiscoveryClient.GetAsync("https://localhost:44388");
            var tokenClient = new TokenClient(disco.TokenEndpoint, "MyClient", "TopSecretClientSecret");
            //var tokenResponse = await tokenClient.RequestClientCredentialsAsync("MyAPI");
            var tokenResponse = await tokenClient.RequestClientCredentialsAsync("MyAPI2");

            var client = new HttpClient();
            client.SetBearerToken(tokenResponse.AccessToken);
            var response = await client.GetAsync("https://localhost:44388/api/data/secure");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();

                ViewData["Message"] = content;
            }
            else
            {
                ViewData["Message"] = response.StatusCode;
            }

            return View();
        }
    }
}