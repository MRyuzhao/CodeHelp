namespace CodeHelp.SSO.Controllers
{
    public class ApiConfig
    {
        public string Name { get; set; }
        public string DisplayName { get; set; }
    }

    public class ClientConfig
    {
        public string ClientId { get; set; }
        public string ClientName { get; set; }
        public string ClientSecret { get; set; }
        public string[] RedirectUris { get; set; }
        public string[] PostLogoutRedirectUris { get; set; }
        public string[] AllowedCorsOrigins { get; set; }
        public string[] AllowedScopes { get; set; }
    }
}