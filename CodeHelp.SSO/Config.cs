using System.Collections.Generic;
using System.Security.Claims;
using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;

namespace CodeHelp.SSO
{
    public class Config
    {
        // scopes define the resources in your system
        public IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                //new IdentityResources.Email(),
            };
        }

        public IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource("codeHelpApis", "codeHelp.API")
                //{
                //    UserClaims = new [] { "email" }
                //}
            };
        }

        // clients want to access resources (aka scopes)
        public IEnumerable<IdentityServer4.Models.Client> GetClients() =>
            // client credentials client
            new List<IdentityServer4.Models.Client>
            {
                //限定client可以访问哪些信息
                new IdentityServer4.Models.Client
                {
                    ClientId = "codeHelpClient",
                    ClientName = "codeHelp.Client",
                    AllowedGrantTypes = GrantTypes.Implicit,
                    AllowAccessTokensViaBrowser = true,//允许返回Access Token
                    //AccessTokenLifetime = 60 * 10,
                    ClientSecrets = { new Secret("secret".Sha256()) },
                    RedirectUris = { "http://localhost:5002/loginCallback" },//须与客户端一致
                    PostLogoutRedirectUris = { "http://localhost:5002/logoutCallback" },//须与客户端一致
                    RequireConsent = false,//禁用 consent 页面确认
                    AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "codeHelpApis"//todo api resource
                    },
                    AllowedCorsOrigins=new List<string>//支持跨域
                    {
                        "http://localhost:5002",
                        "http://localhost:5001",
                        "http://localhost:5000"
                    },
                 
                },

                //new IdentityServer4.Models.Client
                //{
                //    ClientId = "hybridFlowClient",
                //    ClientName = "hybridFlow.Client",
                //    AllowedGrantTypes = GrantTypes.HybridAndClientCredentials,
                //    ClientSecrets =
                //    {
                //        new Secret("secret".Sha256())
                //    },

                //    RedirectUris = { "http://localhost:5002/signin-oidc" },
                //    PostLogoutRedirectUris = { "http://localhost:5002/signout-callback-oidc" },

                //    AllowedScopes =
                //    {
                //        IdentityServerConstants.StandardScopes.OpenId,
                //        IdentityServerConstants.StandardScopes.Profile,
                //        IdentityServerConstants.StandardScopes.Email,
                //        "codeHelpApis"
                //    },
                //    AllowOfflineAccess = true,//todo 获取Refresh Token,
                //    AllowAccessTokensViaBrowser = true
                //}
            };

        public List<TestUser> GetUsers()
        {
            return new List<TestUser>
            {
                new TestUser
                {
                    SubjectId = "1",
                    Username = "admin",
                    Password = "password",

                    Claims = new List<Claim>
                    {
                        new Claim("name", "admins"),
                        //new Claim("email", "mail@qq.com")
                    }
                }
            };
        }
    }
}