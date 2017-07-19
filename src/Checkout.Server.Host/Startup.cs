using System.Linq;
using System.Net.Http.Formatting;
using System.Web.Http;
using System.Web.Mvc;
using Checkout.Server.Ioc.Registrars;
using FluentValidation.WebApi;
using Newtonsoft.Json.Serialization;
using Owin;

namespace Checkout.Server.Host
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var config = new HttpConfiguration();

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
        }
    }

}