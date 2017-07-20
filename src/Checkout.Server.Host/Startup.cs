using System;
using System.Linq;
using System.Net.Http.Formatting;
using System.Web.Http;
using Checkout.Server.Api.Filters;
using Checkout.Server.Host.OAuth;
using Checkout.Server.Ioc.Registrars;
using FluentValidation.WebApi;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.DataHandler.Encoder;
using Microsoft.Owin.Security.Jwt;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json.Serialization;
using Owin;

namespace Checkout.Server.Host
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var config = new HttpConfiguration();

            ConfigureDummyOAuth(app);
            ConfigurationHttp(config);

            var container = new WindsorContainerConfigurator().Configure(config);

            FluentValidationModelValidatorProvider
                .Configure(config, provider => provider.ValidatorFactory = new WindsorValidatorFactory(container));

            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
            app.UseWebApi(config);
        }

        public virtual void ConfigurationHttp(HttpConfiguration config)
        {
            WebApiConfig.Register(config);
            FormatterConfig.Register(config);
            SwaggerConfig.Register(config);

            var jsonFormatter = config.Formatters.OfType<JsonMediaTypeFormatter>().First();
            jsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

            config.Filters.Add(new ValidateModelStateFilter());
        }

        public void ConfigureDummyOAuth(IAppBuilder app)
        {
            //todo: use Formo here to grab these setting from  web config
            var issuer = "dummy-oauth-server";
            var audienceUsers = "099153c2625149bc8ecb3e85e03f0022";
            var audienceClients = "9f6858626c094554b31ebefa4b8cac2c";
            var secret = TextEncodings.Base64Url.Decode("IxrAjDoa2FqElO7IhrSrUJELhUckePEPVpaePlS_Xaw");
            
            app.UseOAuthAuthorizationServer(new OAuthAuthorizationServerOptions()
            {
                //For Dev enviroment only (on production should be AllowInsecureHttp = false)
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/oauth/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromMinutes(30),
                Provider = new DummyOAuthProvider(),
                AccessTokenFormat = new DummyJwtFormat(issuer)
            });
            
            app.UseJwtBearerAuthentication(
                new JwtBearerAuthenticationOptions
                {
                    AuthenticationMode = AuthenticationMode.Active,
                    AllowedAudiences = new[] { audienceUsers, audienceClients },

                    IssuerSecurityTokenProviders = new IIssuerSecurityTokenProvider[]
                    {
                        new SymmetricKeyIssuerSecurityTokenProvider(issuer, secret)
                    }
                });
        }
    }

}