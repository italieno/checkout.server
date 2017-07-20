using System;
using System.Collections.Concurrent;
using System.Security.Cryptography;
using Microsoft.Owin.Security.DataHandler.Encoder;

namespace Checkout.Server.Host.OAuth
{
    public static class AudiencesStore
    {
        public static ConcurrentDictionary<string, Audience> AudiencesList = new ConcurrentDictionary<string, Audience>();

        static AudiencesStore()
        {
            AudiencesList.TryAdd("099153c2625149bc8ecb3e85e03f0022",
                new Audience
                {
                    ClientId = "099153c2625149bc8ecb3e85e03f0022",
                    Base64Secret = "IxrAjDoa2FqElO7IhrSrUJELhUckePEPVpaePlS_Xaw",
                    Name = "SimpleClient"
                });
        }
        
        public static Audience FindAudience(string clientId)
        {
            Audience audience = null;
            if (AudiencesList.TryGetValue(clientId, out audience))
            {
                return audience;
            }
            return null;
        }
    }
}