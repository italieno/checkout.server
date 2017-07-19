using System;
using System.Web.Http;

namespace Checkout.Server.Host
{
    public static class FormatterConfig
    {
        public static void Register(HttpConfiguration httpConfiguration)
        {
            if (httpConfiguration == null) throw new ArgumentNullException(nameof(httpConfiguration));

            httpConfiguration.Formatters.Remove(httpConfiguration.Formatters.XmlFormatter);
        }
    }
}