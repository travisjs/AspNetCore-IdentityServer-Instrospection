using IdentityServer4.Models;
using System.Collections.Generic;

namespace AspNet.Authentication.IdentityServer
{
    public class Config
    {
        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                // this will leave out the ApiScope record in the database
                new ApiResource
                {
                    Name = "MyAPI",
                    DisplayName = "My API",
                    ApiSecrets =
                    {
                        new Secret("TopSecret".Sha256()),
                    }
                },
                // this will correctly create the ApiResource, ApiScope, and ApiSecret records in the database.
                new ApiResource("MyAPI2", "My API2")
                {
                    ApiSecrets =
                    {
                        new Secret("TopSecret".Sha256())
                    }
                }
            };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
                new Client
                {
                    ClientName = "My Client",
                    AlwaysSendClientClaims = true,
                    ClientId = "MyClient",
                    // changed the secret to make clear it is different than the Api secrets
                    ClientSecrets = { new Secret("TopSecretClientSecret".Sha256()) },
                    // RequireClientSecret might as well be true if you are giving this client a secret
                    RequireClientSecret = true,
                    AllowAccessTokensViaBrowser = true,
                    AllowedGrantTypes = GrantTypes.HybridAndClientCredentials,
                    // Added MyAPI2 from my example above
                    AllowedScopes = { "MyAPI", "MyAPI2" },
                    RequireConsent = false,
                    AllowOfflineAccess = true
                }
            };
        }
    }
}