using System.Reflection;
using System.Web.Http;
using Swashbuckle.Application;

namespace Checkout.Server.Host
{
    public class SwaggerConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config
                .EnableSwagger(c =>
                {
                    c.SingleApiVersion("v1", Assembly.GetExecutingAssembly().GetName().Name);
                    c.IgnoreObsoleteActions();
                    c.DescribeAllEnumsAsStrings();
                })
                .EnableSwaggerUi("reference/{*assetPath}", c =>
                {
                    c.DocExpansion(DocExpansion.Full);
                });


        }
    }
}