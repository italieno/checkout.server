﻿using System.Collections.Concurrent;

namespace Checkout.Server.Host.OAuth
{
    public static class AudiencesStore
    {
        public static ConcurrentDictionary<string, Audience> AudiencesList = new ConcurrentDictionary<string, Audience>();

        static AudiencesStore()
        {
            //todo: use Formo here to grab these setting from  web config
            AudiencesList.TryAdd("099153c2625149bc8ecb3e85e03f0022",
                new Audience
                {
                    ClientId = "099153c2625149bc8ecb3e85e03f0022",
                    Base64Secret = "IxrAjDoa2FqElO7IhrSrUJELhUckePEPVpaePlS_Xaw",
                    Name = "SimpleClient"
                });

            AudiencesList.TryAdd("9f6858626c094554b31ebefa4b8cac2c",
                new Audience
                {
                    ClientId = "9f6858626c094554b31ebefa4b8cac2c",
                    Base64Secret = "IxrAjDoa2FqElO7IhrSrUJELhUckePEPVpaePlS_Xaw",
                    Name = "TrustedClient",
                    IsConfidential = true
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