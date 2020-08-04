using System;
using System.IdentityModel.Tokens;
using System.Net;
using System.Threading.Tasks;
using IdentityServer3.AccessTokenValidation;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(cobamvc.Startup))]

namespace cobamvc
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ServicePointManager.ServerCertificateValidationCallback += (o, c, ch, er) => true; //bypass all certificate
            //JwtSecurityTokenHandler.InboundClaimTypeMap.Clear();
            app.UseIdentityServerBearerTokenAuthentication(new IdentityServerBearerTokenAuthenticationOptions()
            {
                Authority = "https://localhost:44308",
                ClientId = "5adc053b-8d5c-48d1-9c0e-180e5cd491d50f7B",
                ClientSecret = "2g7WCZnMFOZdIBklFwTnS4H",
                RequiredScopes = new[] { "cb", "general_approval" }, //perlu ditambah general_approval
                ValidationMode = ValidationMode.ValidationEndpoint,
                
            });
        }
    }
}
