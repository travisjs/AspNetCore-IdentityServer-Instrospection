using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AspNet.Authentication.IdentityServer.Controllers
{
    [Route("api/[controller]/[action]")]
    [Produces("application/json")]
    public class DataController : Controller
    {
        [HttpGet]
        public IEnumerable<String> Public()
        {
            return new[] { "public1", "public2" };
        }

        [HttpGet]
        [Authorize]
        public async Task<IEnumerable<String>> Secure()
        {
            var accessToken = await HttpContext.Authentication.GetTokenAsync("access_token");

            var introspectionClient = new IntrospectionClient("https://localhost:44388/connect/introspect", "MyAPI", "TopSecret");
            //var introspectionClient = new IntrospectionClient("https://localhost:44388/connect/introspect", "MyAPI2", "TopSecret");

            var response = await introspectionClient.SendAsync(new IntrospectionRequest { Token = accessToken });

            var isActive = response.IsActive;
            var claims = response.Claims;

            return new[] { "secure1", "secure2", $"isActive: {isActive}", JsonConvert.SerializeObject(claims) };
        }
    }
}